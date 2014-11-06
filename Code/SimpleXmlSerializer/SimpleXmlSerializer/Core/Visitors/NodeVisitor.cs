using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    internal abstract class NodeVisitor
    {
        private readonly XmlSerializerSettings settings;
        private readonly IDictionary<Type, INode> nodesCache;

        protected NodeVisitor(XmlSerializerSettings settings, IDictionary<Type, INode> nodesCache)
        {
            this.settings = settings;
            this.nodesCache = nodesCache;
        }

        protected virtual INode GetNode(Type type)
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

        protected NodeName GetNodeName(Type type)
        {
            return settings.NameProvider.GetNodeName(type);
        }

        protected NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            return settings.NameProvider.GetNodeName(propertyInfo);
        }
    }
}