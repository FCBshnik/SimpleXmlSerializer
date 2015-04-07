using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide and cache info about object graph.
    /// </summary>
    internal class NodeProvider
    {
        private readonly XmlSerializerSettings settings;
        private readonly IDictionary<Type, INode> nodesCache = new Dictionary<Type, INode>();

        public NodeProvider(XmlSerializerSettings settings)
        {
            this.settings = settings;
        }

        public INode GetNode(Type type)
        {
            if (nodesCache.ContainsKey(type))
            {
                return (INode)nodesCache[type].Clone();
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
                throw new SerializationException(string.Format("Can not serialize type {0}", type));
            }

            nodesCache[type] = node;
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