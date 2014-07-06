using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using SimpleXmlSerializer.Extensions;
using System.Linq;

namespace SimpleXmlSerializer.Core
{
    public class SerializeToXmlVisitor : INodeVisitor
    {
        private readonly XmlWriter xmlWriter;

        private readonly XmlSerializerSettings settings;

        public SerializeToXmlVisitor(XmlWriter xmlWriter, XmlSerializerSettings settings)
        {
            this.xmlWriter = xmlWriter;
            this.settings = settings;
        }

        public void Visit(object value)
        {
            var valueType = value.GetType();

            xmlWriter.WriteStartDocument();

            var node = GetNode(valueType);
            var nodeName = settings.NameProvider.GetNodeName(valueType);

            node.Value = value;
            node.Name = nodeName;
            
            node.Accept(this);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
        }

        public void Visit(PrimitiveNode node)
        {
            var value = node.TypeDescription.Serializer.Serialize(node.Value);

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
            xmlWriter.WriteStartElement(node.Name.ElementName);

            var itemNode = GetNode(node.TypeDescription.ItemType);
            var itemNodeName = settings.NameProvider.GetNodeName(node.TypeDescription.ItemType);

            foreach (var item in ((IEnumerable)node.Value).SkipNulls())
            {
                // note: passing second parameter to NodeName ctor is not clear
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);
                itemNode.Value = item;
                itemNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(ComplexNode node)
        {
            xmlWriter.WriteStartElement(node.Name.ElementName);

            var properties = node.TypeDescription.Properties
                .ToDictionary(pi => settings.NameProvider.GetNodeName(pi), pi => pi);

            // some properties may be presented as attribute and as element
            // here we give precedence to attributes
            properties = properties
                .Where(p => p.Key.IsAttribute)
                .Concat(properties.Where(p => !p.Key.IsAttribute))
                .ToDictionary(p => p.Key, p => p.Value);

            foreach (var pair in properties)
            {
                var propertyInfo = pair.Value;
                var propertyValue = propertyInfo.GetValue(node.Value, null);
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyNode = GetNode(propertyInfo.PropertyType);
                var propertyNodeName = settings.NameProvider.GetNodeName(propertyInfo);

                propertyNode.Value = propertyValue;
                propertyNode.Name = propertyNodeName;
                
                propertyNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(CustomNode node)
        {
            xmlWriter.WriteStartElement(node.Name.ElementName);

            node.Serializer.Serialize(node.Value, xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private INode GetNode(Type valueType)
        {
            INode node;

            ICustomSerializer customSerializer;
            PrimitiveDescription primitiveDescription;
            CollectionNodeDescription collectionDescription;

            if(settings.CustomProvider.TryGetCustomSerializer(valueType, out customSerializer))
            {
                node = new CustomNode
                {
                    Serializer = customSerializer
                };
            }
            else if (settings.PrimitiveProvider.TryGetPrimitiveDescription(valueType, out primitiveDescription))
            {
                node = new PrimitiveNode
                    {
                        TypeDescription = primitiveDescription,
                    };
            }
            else if (settings.CollectionProvider.TryGetDescription(valueType, out collectionDescription))
            {
                node = new CollectionNode
                    {
                        TypeDescription = collectionDescription,
                    };
            }
            else
            {
                node = new ComplexNode
                    {
                        TypeDescription = settings.ComplexProvider.GetDescription(valueType),
                    };
            }

            return node;
        }
    }
}