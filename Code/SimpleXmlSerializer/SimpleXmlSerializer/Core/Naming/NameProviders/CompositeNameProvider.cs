using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="INameProvider"/> which collects and merges
    /// node name from collection of <see cref="INameProvider"/>. 
    /// Provider which goes first gets precedence.
    /// </summary>
    public class CompositeNameProvider : INameProvider
    {
        private readonly IEnumerable<INameProvider> providers;

        public CompositeNameProvider(IEnumerable<INameProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public NodeName GetNodeName(Type type)
        {
            var nodeName = NodeName.Empty;
            foreach (var provider in providers)
            {
                nodeName = Merge(nodeName, provider.GetNodeName(type));
            }

            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var nodeName = NodeName.Empty;
            foreach (var provider in providers)
            {
                nodeName = Merge(nodeName, provider.GetNodeName(propertyInfo));
            }

            return nodeName;
        }

        private static NodeName Merge(NodeName to, NodeName from)
        {
            var elementName = to.HasElementName ? to.ElementName : from.ElementName;
            var attributeName = to.HasAttributeName ? to.AttributeName : from.AttributeName;
            var itemName = to.HastItemName ? to.ItemName : @from.ItemName;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}