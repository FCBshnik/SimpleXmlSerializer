using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public class PrimitiveToAttributeNameProvider : INameProvider
    {
        private readonly IPrimitiveTypeProvider primitiveProvider;
        private readonly INameProvider underlyingProvider;

        public PrimitiveToAttributeNameProvider(INameProvider underlyingProvider, IPrimitiveTypeProvider primitiveProvider)
        {
            this.primitiveProvider = primitiveProvider;
            this.underlyingProvider = underlyingProvider;
        }

        public NodeName GetNodeName(Type type)
        {
            var name = underlyingProvider.GetNodeName(type);

            if (!name.HasAttributeName && IsPrimitive(type))
            {
                return new NodeName(null, null, name.ElementName);
            }

            return name;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var name = underlyingProvider.GetNodeName(propertyInfo);

            if (!name.HasAttributeName && IsPrimitive(propertyInfo.PropertyType))
            {
                return new NodeName(null, null, name.ElementName);
            }

            return name;
        }

        private bool IsPrimitive(Type type)
        {
            PrimitiveTypeDescription primitiveDescription;
            if (primitiveProvider.TryGetDescription(type, out primitiveDescription))
            {
                return true;
            }

            return false;
        }
    }
}