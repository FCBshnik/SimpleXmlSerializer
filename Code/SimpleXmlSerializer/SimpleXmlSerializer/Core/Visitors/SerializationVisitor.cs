﻿using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using SimpleXmlSerializer.Extensions;
using System.Linq;

namespace SimpleXmlSerializer.Core
{
    internal class SerializationVisitor : NodeVisitor, INodeVisitor
    {
        private readonly XmlSerializerSettings settings;
        private readonly XmlWriter xmlWriter;

        public SerializationVisitor(XmlWriter xmlWriter, XmlSerializerSettings settings) : base(settings)
        {
            this.xmlWriter = xmlWriter;
            this.settings = settings;
        }

        public void Visit(object value)
        {
            var valueType = value.GetType();

            var node = GetNode(valueType);
            var nodeName = settings.NameProvider.GetNodeName(valueType);
            node.Value = value;
            node.Name = nodeName;

            xmlWriter.WriteStartDocument();
            node.Accept(this);
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
        }

        public void Visit(PrimitiveNode node)
        {
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
            xmlWriter.WriteStartElement(node.Name.ElementName);

            var itemNodeName = settings.NameProvider.GetNodeName(node.Description.ItemType);

            foreach (var item in ((IEnumerable)node.Value).SkipNulls())
            {
                // note: passing second parameter to NodeName ctor is not clear
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                var itemNode = GetNode(node.Description.ItemType);
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);
                itemNode.Value = item;
                itemNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(ComplexNode node)
        {
            xmlWriter.WriteStartElement(node.Name.ElementName);

            var properties = node.Description.Properties
                .ToDictionary(pi => settings.NameProvider.GetNodeName(pi), pi => pi);

            // some properties may be presented as attributes and as element
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

                var propertyNodeName = settings.NameProvider.GetNodeName(propertyInfo);
                var propertyNode = GetNode(propertyInfo.PropertyType);
                propertyNode.Value = propertyValue;
                propertyNode.Name = propertyNodeName;
                
                propertyNode.Accept(this);
            }

            xmlWriter.WriteEndElement();
        }

        public void Visit(CustomNode node)
        {
            xmlWriter.WriteStartElement(node.Name.ElementName);

            // todo: somehow validate custom serializer
            node.Description.Serializer.Serialize(node.Value, xmlWriter);

            xmlWriter.WriteEndElement();
        }
    }
}