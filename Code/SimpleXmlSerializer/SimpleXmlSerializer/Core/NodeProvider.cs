using System;
using System.Collections.Generic;
using System.Reflection;

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

        public virtual INode GetNode(Type type)
        {
            if (nodesCache.ContainsKey(type))
            {
                return (INode)nodesCache[type].Clone();
            }

            INode node;
            CustomNodeDescription customNodeDescription;
            PrimitiveNodeDescription primitiveNodeDescription;
            CollectionNodeDescription collectionNodeDescription;

            if (settings.CustomProvider.TryGetDescription(type, out customNodeDescription))
            {
                node = new CustomNode(customNodeDescription);
            }
            else if (settings.PrimitiveProvider.TryGetDescription(type, out primitiveNodeDescription))
            {
                node = new PrimitiveNode(primitiveNodeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(type, out collectionNodeDescription))
            {
                node = new CollectionNode(collectionNodeDescription);
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
    }
}