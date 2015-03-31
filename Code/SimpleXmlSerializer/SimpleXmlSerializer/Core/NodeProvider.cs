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
            PrimitiveNodeDescription primitiveNodeDescription;
            CollectionTypeDescription collectionTypeDescription;

            if (settings.PrimitiveProvider.TryGetDescription(type, out primitiveNodeDescription))
            {
                node = new PrimitiveNode(primitiveNodeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(type, out collectionTypeDescription))
            {
                node = new CollectionNode(collectionTypeDescription);
            }
            else
            {
                var complexNodeDescription = settings.ComplexProvider.GetDescription(type);
                node = new ComplexNode(complexNodeDescription);
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
                .Where(n => n.IsElement)
                .GroupBy(n => n.ElementName)
                .FirstOrDefault(gr => gr.Count() > 1);
            if (conflictedElementNames != null)
            {
                throw new SerializationException(string.Format("There are multiply properties with element name '{0}'", conflictedElementNames.Key));
            }

            var conflictedAttributeNames = nodeNames.Keys
                .Where(n => n.IsAttribute)
                .GroupBy(n => n.AttributeName)
                .FirstOrDefault(gr => gr.Count() > 1);
            if (conflictedAttributeNames != null)
            {
                throw new SerializationException(string.Format("There are multiply properties with attribute name '{0}'", conflictedAttributeNames.Key));
            }

            return nodeNames;
        }

        public Type AdjustType(Type type)
        {
            return type.IsSubclassOf(typeof(Type)) ? typeof(Type) : type;
        }
    }
}