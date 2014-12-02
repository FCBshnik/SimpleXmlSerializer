using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using SimpleXmlSerializer.Extensions;
using System.Linq;

namespace SimpleXmlSerializer.Core
{
    internal class DeserializationVisitor : NodeVisitor, INodeVisitor
    {
        private readonly XmlSerializerSettings settings;
        private readonly XmlReader xmlReader;

        public DeserializationVisitor(XmlReader xmlReader, XmlSerializerSettings settings, IDictionary<Type, INode> nodesCache)
            : base(settings, nodesCache)
        {
            this.xmlReader = xmlReader;
            this.settings = settings;
        }

        public object Visit(Type type)
        {
            var nodeName = settings.NameProvider.GetNodeName(type);

            if (xmlReader.ReadToNextSibling(nodeName.ElementName))
            {
                var node = GetNode(type);
                node.Name = nodeName;
                
                node.Accept(this);

                return node.Value;
            }

            return null;
        }

        public void Visit(PrimitiveNode node)
        {
            if (node.Name.IsElement)
            {
                // distinguish nodes without value (about which indicates IsEmptyElement)
                // and nodes with some value (even if this value is empty string),
                if (xmlReader.IsEmptyElement)
                {
                    node.Value = null;
                }
                else
                {
                    var serializedValue = xmlReader.ReadElementString();
                    node.Value = node.Description.Serializer.Deserialize(serializedValue);
                }
            }
            else if (node.Name.IsAttribute)
            {
                var serializedValue = xmlReader.Value;
                node.Value = node.Description.Serializer.Deserialize(serializedValue);
            }
            else
            {
                throw new SerializationException();
            }
        }

        public void Visit(CollectionNode node)
        {
            var items = new ArrayList();

            if (xmlReader.ReadToDescendant(node.Name.ItemName))
            {
                // note: passing second parameter to NodeName ctor is not clear
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                var itemNode = GetNode(node.Description.ItemType);
                var itemNodeName = settings.NameProvider.GetNodeName(node.Description.ItemType);
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);

                do
                {
                    // todo: think about boxing
                    itemNode.Accept(this);
                    items.Add(itemNode.Value);

                } while (xmlReader.ReadToNextSibling(node.Name.ItemName));
            }

            node.Value = node.Description.Factory(items);
        }

        public void Visit(ComplexNode node)
        {
            var propertyValues = new Dictionary<PropertyInfo, object>();

            var names = node.Description.Properties
                .ToDictionary(pi => settings.NameProvider.GetNodeName(pi), pi => pi);

            // first deserialize from attributes of current element
            if (xmlReader.MoveToFirstAttribute())
            {
                var attributesNames = names
                    .Where(p => p.Key.IsAttribute)
                    .ToDictionary(p => p.Key.AttributeName, p => p.Value);

                do
                {
                    PropertyInfo propertyInfo;
                    if (attributesNames.TryGetValue(xmlReader.LocalName, out propertyInfo))
                    {
                        var propertyNode = GetNode(propertyInfo.PropertyType);
                        var propertyNodeName = settings.NameProvider.GetNodeName(propertyInfo);
                        propertyNode.Name = propertyNodeName;

                        propertyNode.Accept(this);

                        propertyValues[propertyInfo] = propertyNode.Value;
                    }
                } while (xmlReader.MoveToNextAttribute());

                xmlReader.MoveToElement();
            }

            // deserialize from elements
            if (!xmlReader.IsEmptyElement && xmlReader.ReadToDescendant())
            {
                var elementsNames = names
                    .Where(p => p.Key.IsElement)
                    .ToDictionary(p => p.Key.ElementName, p => p.Value);

                do
                {
                    PropertyInfo propertyInfo;
                    if (elementsNames.TryGetValue(xmlReader.LocalName, out propertyInfo))
                    {
                        var propertyNode = GetNode(propertyInfo.PropertyType);
                        var propertyNodeName = settings.NameProvider.GetNodeName(propertyInfo);
                        propertyNode.Name = propertyNodeName;

                        propertyNode.Accept(this);

                        propertyValues[propertyInfo] = propertyNode.Value;
                    }
                } while (xmlReader.ReadToNextSibling());
            }

            node.Value = node.Description.Factory(propertyValues);
        }

        public void Visit(CustomNode node)
        {
            node.Value = node.Description.Serializer.Deserialize(xmlReader);
        }
    }
}