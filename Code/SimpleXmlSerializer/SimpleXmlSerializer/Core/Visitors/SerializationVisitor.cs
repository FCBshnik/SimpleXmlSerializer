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

            if (node.Name.HasElementName)
            {
                xmlWriter.WriteStartElement(node.Name.ElementName.Name);

                switch (node.Description.XmlCharacterType)
                {
                    case XmlCharacterType.Text:
                        xmlWriter.WriteValue(value);
                        break;
                    case XmlCharacterType.CData:
                        xmlWriter.WriteCData(value);
                        break;
                }

                xmlWriter.WriteEndElement();
            }
            else if (node.Name.HasAttributeName)
            {
                xmlWriter.WriteAttributeString(node.Name.AttributeName.Name, value);
            }
            else
            {
                throw new SerializationException("Xml element or attribute name was not provided");
            }
        }

        public void Visit(CollectionNode node)
        {
            OnVisitNode(node);

            xmlWriter.WriteStartElement(node.Name.ElementName.Name);

            var itemNodeName = nodeProvider.GetNodeName(node.Description.ItemType);

            foreach (var item in ((IEnumerable)node.Value))
            {
                // passing second parameter to NodeName ctor is not clear:
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                var itemNode = nodeProvider.GetNode(node.Description.ItemType);
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);
                itemNode.Value = item;
                itemNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(CompositeNode node)
        {
            OnVisitNode(node);

            xmlWriter.WriteStartElement(node.Name.ElementName.Name);

            var properties = nodeProvider.GetNodeNames(node.Description.Properties);

            // some properties may be presented as attributes and as elements
            // give precedence to attributes
            properties = properties
                .Where(p => p.Key.HasAttributeName)
                .Concat(properties.Where(p => !p.Key.HasAttributeName))
                .ToDictionary(p => p.Key, p => p.Value);

            foreach (var pair in properties)
            {
                var propertyInfo = pair.Value;
                var propertyValue = node.Description.PropertyGetter(node.Value, propertyInfo);
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyNodeName = nodeProvider.GetNodeName(propertyInfo);
                var propertyNode = nodeProvider.GetNode(propertyInfo);
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
            var value = node.Value;
            if (value == null)
            {
                return;
            }

            var valueType = value.GetType();
            if (valueType.IsValueType || valueType == typeof(string))
            {
                return;
            }

            if (visitedNodeValues.Contains(value))
            {
                throw new SerializationException("There is circular dependency in object graph");
            }

            visitedNodeValues.Add(value);
        }
    }
}