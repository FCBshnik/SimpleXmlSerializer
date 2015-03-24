using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq;

namespace SimpleXmlSerializer.Core
{
    internal class SerializationVisitor : INodeVisitor
    {
        private readonly XmlWriter xmlWriter;
        private readonly NodeProvider nodeProvider;
        private readonly HashSet<object> visitedNodeValues = new HashSet<object>();

        public SerializationVisitor(XmlWriter xmlWriter, NodeProvider nodeProvider)
        {
            this.xmlWriter = xmlWriter;
            this.nodeProvider = nodeProvider;
        }

        public void Visit(object value)
        {
            var valueType = value.GetType();

            var node = nodeProvider.GetNode(valueType);
            var nodeName = nodeProvider.GetNodeName(valueType);
            node.Value = value;
            node.Name = nodeName;

            xmlWriter.WriteStartDocument();
            node.Accept(this);
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
        }

        public void Visit(PrimitiveNode node)
        {
            OnVisitNode(node);

            var value = node.Description.Serializer.Serialize(node.Value);

            if (node.Name.IsElement)
            {
                xmlWriter.WriteStartElement(node.Name.ElementName);
                xmlWriter.WriteValue(value);
                xmlWriter.WriteEndElement();
            }
            else if (node.Name.IsAttribute)
            {
                xmlWriter.WriteAttributeString(node.Name.AttributeName, value);
            }
            else
            {
                throw new SerializationException();
            }
        }

        public void Visit(CollectionNode node)
        {
            OnVisitNode(node);

            xmlWriter.WriteStartElement(node.Name.ElementName);

            var itemNodeName = nodeProvider.GetNodeName(node.Description.ItemType);

            // todo: deal with nulls
            foreach (var item in ((IEnumerable)node.Value))
            {
                // note: passing second parameter to NodeName ctor is not clear
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                var itemNode = nodeProvider.GetNode(node.Description.ItemType);
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);
                itemNode.Value = item;
                itemNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(ComplexNode node)
        {
            OnVisitNode(node);

            xmlWriter.WriteStartElement(node.Name.ElementName);

            var properties = nodeProvider.GetNodeNames(node.Description.Properties);

            // some properties may be presented as attributes and as element
            // here we give precedence to attributes
            properties = properties
                .Where(p => p.Key.IsAttribute)
                .Concat(properties.Where(p => !p.Key.IsAttribute))
                .ToDictionary(p => p.Key, p => p.Value);

            foreach (var pair in properties)
            {
                var propertyInfo = pair.Value;
                var propertyValue = node.Description.Getter(node.Value, propertyInfo);
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyNodeName = nodeProvider.GetNodeName(propertyInfo);
                var propertyNode = nodeProvider.GetNode(propertyInfo.PropertyType);
                propertyNode.Value = propertyValue;
                propertyNode.Name = propertyNodeName;

                propertyNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        private void OnVisitNode(INode node)
        {
            TrackCircularDependency(node);
        }

        private void TrackCircularDependency(INode node)
        {
            if (node.Value == null)
            {
                return;
            }

            var valueType = node.Value.GetType();
            if (valueType.IsValueType || valueType == typeof(string))
            {
                return;
            }

            if (visitedNodeValues.Contains(node.Value))
            {
                throw new SerializationException("There is circular dependency in object graph");
            }

            visitedNodeValues.Add(node.Value);
        }
    }
}