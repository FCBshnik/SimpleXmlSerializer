using System;
using SimpleXmlSerializer.Core.Serializers;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize enumeration types.
    /// </summary>
    public class EnumPrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        public bool TryGetDescription(Type type, out PrimitiveTypeDescription primitiveDescription)
        {
            if (type.IsEnum)
            {
                primitiveDescription = new PrimitiveTypeDescription(new EnumSerializer(type));
                return true;
            }

            primitiveDescription = null;
            return false;
        }
    }
}