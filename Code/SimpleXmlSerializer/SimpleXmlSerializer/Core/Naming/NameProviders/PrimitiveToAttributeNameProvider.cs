using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The decorator over <see cref="INameProvider"/> which moves element name
    /// to attribute name of <see cref="NodeName"/> for primitive types. Primitive types
    /// are detected using specified <see cref="IPrimitiveTypeProvider"/>.
    /// </summary>
    internal class PrimitiveToAttributeNameProvider : INameProvider
    {
        private readonly INameProvider nameProvider;
        private readonly IPrimitiveTypeProvider primitiveProvider;

        public PrimitiveToAttributeNameProvider(INameProvider nameProvider, IPrimitiveTypeProvider primitiveProvider)
        {
            if (nameProvider == null) 
                throw new ArgumentNullException("nameProvider");
            if (primitiveProvider == null) 
                throw new ArgumentNullException("primitiveProvider");

            this.primitiveProvider = primitiveProvider;
            this.nameProvider = nameProvider;
        }

        public NodeName GetNodeName(Type type)
        {
            var nodeName = nameProvider.GetNodeName(type);
            if (!nodeName.HasAttributeName && nodeName.HasElementName && IsPrimitive(type))
            {
                return new NodeName(null, null, nodeName.ElementName);
            }

            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var nodeName = nameProvider.GetNodeName(propertyInfo);
            if (!nodeName.HasAttributeName && nodeName.HasElementName && IsPrimitive(propertyInfo.PropertyType))
            {
                return new NodeName(null, null, nodeName.ElementName);
            }

            return nodeName;
        }

        private bool IsPrimitive(Type type)
        {
            PrimitiveTypeDescription primitiveDescription;
            return primitiveProvider.TryGetDescription(type, out primitiveDescription);
        }
    }
}