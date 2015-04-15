using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide and cache info about modes of object graph.
    /// </summary>
    internal class NodeProvider
    {
        private readonly XmlSerializerSettings settings;
        private readonly IDictionary<Type, INode> nodesCacheByType = new Dictionary<Type, INode>();
        private readonly IDictionary<PropertyInfo, INode> nodesCacheByProperty = new Dictionary<PropertyInfo, INode>();

        public NodeProvider(XmlSerializerSettings settings)
        {
            this.settings = settings;
        }

        public INode GetNode(Type type)
        {
            if (nodesCacheByType.ContainsKey(type))
            {
                return (INode)nodesCacheByType[type].Clone();
            }

            INode node;
            PrimitiveTypeDescription primitiveTypeDescription;
            CollectionTypeDescription collectionTypeDescription;
            CompositeTypeDescription compositeTypeDescription;

            if (settings.PrimitiveProvider.TryGetDescription(type, out primitiveTypeDescription))
            {
                node = new PrimitiveNode(primitiveTypeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(type, out collectionTypeDescription))
            {
                node = new CollectionNode(collectionTypeDescription);
            }
            else if (settings.CompositeProvider.TryGetDescription(type, out compositeTypeDescription))
            {
                node = new CompositeNode(compositeTypeDescription);
            }
            else
            {
                throw new SerializationException(string.Format("Can not serialize type '{0}'", type));
            }

            nodesCacheByType[type] = node;
            return (INode)node.Clone();
        }

        public INode GetNode(PropertyInfo propertyInfo)
        {
            if (nodesCacheByProperty.ContainsKey(propertyInfo))
            {
                return (INode)nodesCacheByProperty[propertyInfo].Clone();
            }

            INode node;
            PrimitiveTypeDescription primitiveTypeDescription;
            CollectionTypeDescription collectionTypeDescription;
            CompositeTypeDescription compositeTypeDescription;

            if (settings.PrimitiveProvider.TryGetDescription(propertyInfo, out primitiveTypeDescription))
            {
                node = new PrimitiveNode(primitiveTypeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(propertyInfo, out collectionTypeDescription))
            {
                node = new CollectionNode(collectionTypeDescription);
            }
            else if (settings.CompositeProvider.TryGetDescription(propertyInfo, out compositeTypeDescription))
            {
                node = new CompositeNode(compositeTypeDescription);
            }
            else
            {
                throw new SerializationException(string.Format("Can not serialize property '{0}'", propertyInfo));
            }

            nodesCacheByProperty[propertyInfo] = node;
            return (INode)node.Clone();
        }

        public NodeName GetNodeName(Type type)
        {
            return settings.NameProvider.GetNodeName(type);
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            return settings.NameProvider.GetNodeName(propertyInfo);
        }

        public IDictionary<NodeName, PropertyInfo> GetNodeNames(IEnumerable<PropertyInfo> properties)
        {
            var nodeNames = properties.ToDictionary(GetNodeName, pi => pi);

            var conflictedElementNames = nodeNames.Keys
                .Where(n => n.HasElementName)
                .GroupBy(n => n.ElementName.Name)
                .FirstOrDefault(gr => gr.Count() > 1);
            if (conflictedElementNames != null)
            {
                throw new SerializationException(string.Format("There are multiply properties with element name '{0}'", conflictedElementNames.Key));
            }

            var conflictedAttributeNames = nodeNames.Keys
                .Where(n => n.HasAttributeName)
                .GroupBy(n => n.AttributeName.Name)
                .FirstOrDefault(gr => gr.Count() > 1);
            if (conflictedAttributeNames != null)
            {
                throw new SerializationException(string.Format("There are multiply properties with attribute name '{0}'", conflictedAttributeNames.Key));
            }

            return nodeNames;
        }
    }
}