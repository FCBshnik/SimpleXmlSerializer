﻿using System;
using System.Reflection;
using SimpleXmlSerializer.Core.Serializers;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize enumeration types.
    /// </summary>
    public class EnumPrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        public bool TryGetDescription(Type type, out PrimitiveTypeDescription description)
        {
            if (type.IsEnum)
            {
                description = new PrimitiveTypeDescription(new EnumSerializer(type));
                return true;
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out PrimitiveTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }
    }
}