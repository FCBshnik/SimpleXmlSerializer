using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.Core
{
    public class CompositeNameProvider : INameProvider
    {
        private readonly IEnumerable<INameProvider> providers;

        public CompositeNameProvider(IEnumerable<INameProvider> providers)
        {
            this.providers = providers;
        }

        public NodeName GetNodeName(Type type)
        {
            var resultName = NodeName.Empty;
            foreach (var provider in providers)
            {
                resultName = Merge(resultName, provider.GetNodeName(type));
            }

            if (!IsValid(resultName))
            {
                throw new SerializationException();
            }

            return resultName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var resultName = NodeName.Empty;
            foreach (var provider in providers)
            {
                resultName = Merge(resultName, provider.GetNodeName(propertyInfo));
            }

            if (!IsValid(resultName))
            {
                throw new SerializationException();
            }

            return resultName;
        }

        private static NodeName Merge(NodeName to, NodeName from)
        {
            var elementName = (to.IsElement || to.IsAttribute) ? to.ElementName : from.ElementName;
            var attributeName = (to.IsElement || to.IsAttribute) ? to.AttributeName : from.AttributeName;
            var itemName = to.IsItem ? to.ItemName : @from.ItemName;

            return new NodeName(elementName, itemName, attributeName);
        }

        private static bool IsValid(NodeName name)
        {
            return name.IsElement || name.IsAttribute;
        }
    }
}