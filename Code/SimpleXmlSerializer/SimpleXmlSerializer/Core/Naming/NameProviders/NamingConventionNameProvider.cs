using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The decorator over <see cref="INameProvider"/> which modifies 
    /// node name using specified <see cref="INamingConvention"/>.
    /// </summary>
    public class NamingConventionNameProvider : INameProvider
    {
        private readonly INameProvider provider;
        private readonly INamingConvention namingConvention;

        public NamingConventionNameProvider(INameProvider provider, INamingConvention namingConvention)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (namingConvention == null)
                throw new ArgumentNullException("namingConvention");

            this.provider = provider;
            this.namingConvention = namingConvention;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var nodeName = provider.GetNodeName(propertyInfo);
            return ApplyNamingConvention(nodeName);
        }

        public NodeName GetNodeName(Type type)
        {
            var nodeName = provider.GetNodeName(type);
            return ApplyNamingConvention(nodeName);
        }

        private NodeName ApplyNamingConvention(NodeName nodeName)
        {
            var elementName = nodeName.HasElementName ? namingConvention.NormalizeName(nodeName.ElementName.Name) : null;
            var itemName = nodeName.HastItemName ? namingConvention.NormalizeName(nodeName.ItemName.Name) : null;
            var attributeName = nodeName.HasAttributeName ? namingConvention.NormalizeName(nodeName.AttributeName.Name) : null;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}