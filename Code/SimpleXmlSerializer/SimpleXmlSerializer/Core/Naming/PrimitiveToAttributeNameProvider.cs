using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public class PrimitiveToAttributeNameProvider : INameProvider
    {
        private readonly IPrimitiveProvider primitiveProvider;

        private readonly INameProvider underlyingProvider;

        public PrimitiveToAttributeNameProvider(INameProvider underlyingProvider, IPrimitiveProvider primitiveProvider)
        {
            this.primitiveProvider = primitiveProvider;
            this.underlyingProvider = underlyingProvider;
        }

        public NodeName GetNodeName(Type type)
        {
            var name = underlyingProvider.GetNodeName(type);

            if (!name.IsAttribute && IsPrimitive(type))
            {
                return new NodeName(string.Empty, string.Empty, name.ElementName);
            }

            return name;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var name = underlyingProvider.GetNodeName(propertyInfo);

            if (!name.IsAttribute && IsPrimitive(propertyInfo.PropertyType))
            {
                return new NodeName(string.Empty, string.Empty, name.ElementName);
            }

            return name;
        }

        private bool IsPrimitive(Type type)
        {
            PrimitiveDescription primitiveDescription;
            if (primitiveProvider.TryGetPrimitiveDescription(type, out primitiveDescription))
            {
                return true;
            }

            return false;
        }
    }
}