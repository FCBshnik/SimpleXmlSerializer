﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Uses specified <see cref="IPropertiesProvider"/> to handle KeyValuePair type.
    /// </summary>
    internal class KeyValuePairCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IPropertiesProvider propertiesProvider;

        public KeyValuePairCompositeTypeProvider(IPropertiesProvider propertiesProvider)
        {
            if (propertiesProvider == null) 
                throw new ArgumentNullException("propertiesProvider");

            this.propertiesProvider = propertiesProvider;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);

                var properties = propertiesProvider.GetProperties(type).ToList();
                var getters = properties.ToDictionary(p => p, ExpressionUtils.GetPropertyGetter);

                description = new CompositeTypeDescription(properties, ps => CreateObject(ctor, ps), (obj, pi) => getters[pi](obj));
                return true;
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out CompositeTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }

        private static object CreateObject(ConstructorInfo ctor, IDictionary<PropertyInfo, object> properties)
        {
            return ctor.Invoke(properties.Values.ToArray());
        }
    }
}