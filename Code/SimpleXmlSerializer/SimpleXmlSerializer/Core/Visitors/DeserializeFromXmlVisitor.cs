using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using SimpleXmlSerializer.Core.Collections;
using SimpleXmlSerializer.Core.Custom;
using SimpleXmlSerializer.Core.Naming;
using SimpleXmlSerializer.Core.Nodes;
using SimpleXmlSerializer.Core.Primitives;
using SimpleXmlSerializer.Extensions;
using System.Linq;

namespace SimpleXmlSerializer.Core.Visitors
{
    public class DeserializeFromXmlVisitor : INodeVisitor
    {
        private readonly XmlReader xmlReader;

        private readonly XmlSerializerSettings settings;

        private object result;

        public DeserializeFromXmlVisitor(XmlReader xmlReader, XmlSerializerSettings settings)
        {
            this.xmlReader = xmlReader;
            this.settings = settings;
        }

        public object GetResult()
        {
            return result;
        }

        public void Visit(Type type)
        {
            var nodeName = settings.NameProvider.GetNodeName(type);

            if (xmlReader.ReadToNextSibling(nodeName.ElementName))
            {
                var node = GetNode(type);
                node.Name = nodeName;
                
                node.Accept(this);

                result = node.Value;
            }
        }

        public void Visit(PrimitiveNode node)
        {
            if (node.Name.IsElement)
            {
                var serializedValue = xmlReader.ReadElementString();
                node.Value = node.TypeDescription.Serializer.Deserialize(serializedValue);
            }
            else if (node.Name.IsAttribute)
            {
                var serializedValue = xmlReader.Value;
                node.Value = node.TypeDescription.Serializer.Deserialize(serializedValue);
            }
            else
            {
                throw new SerializationException();
            }
        }

        public void Visit(CollectionNode node)
        {
            if (xmlReader.ReadToDescendant(node.Name.ItemName))
            {
                var items = new ArrayList();

                // note: passing second parameter to NodeName ctor is not clear
                // we pass such parameter for more clear xml output and it does
                // make sense only for collection of collections
                var itemNode = GetNode(node.TypeDescription.ItemType);
                var itemNodeName = settings.NameProvider.GetNodeName(node.TypeDescription.ItemType);
                itemNode.Name = new NodeName(node.Name.ItemName, itemNodeName.ItemName);

                do
                {
                    itemNode.Accept(this);
                    items.Add(itemNode.Value);

                } while (xmlReader.ReadToNextSibling(node.Name.ItemName));

                node.Value = node.TypeDescription.Factory(items);
            }
        }

        public void Visit(ComplexNode node)
        {
            var propertyValues = new Dictionary<PropertyInfo, object>();

            var names = node.TypeDescription.Properties
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

            node.Value = node.TypeDescription.Factory(propertyValues);
        }

        public void Visit(CustomNode node)
        {
            node.Value = node.Serializer.Deserialize(xmlReader);
        }

        private INode GetNode(Type valueType)
        {
            INode node;

            ICustomSerializer customSerializer;
            PrimitiveDescription primitiveDescription;
            CollectionDescription collectionDescription;

            if (settings.CustomProvider.TryGetCustomSerializer(valueType, out customSerializer))
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
            else if (settings.CollectionProvider.TryGetCollectionDescription(valueType, out collectionDescription))
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