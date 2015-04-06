using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Uses specified <see cref="IPropertiesSelector"/> to handle KeyValuePair type.
    /// </summary>
    internal class KeyValuePairCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IPropertiesSelector propertiesSelector;

        public KeyValuePairCompositeTypeProvider(IPropertiesSelector propertiesSelector)
        {
            if (propertiesSelector == null) 
                throw new ArgumentNullException("propertiesSelector");

            this.propertiesSelector = propertiesSelector;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);

                var properties = propertiesSelector.SelectProperties(type).ToList();
                var getters = properties.ToDictionary(p => p, ExpressionUtils.GetPropertyGetter);

                description = new CompositeTypeDescription(properties, ps => CreateObject(ctor, ps), (obj, pi) => getters[pi](obj));
                return true;
            }

            description = null;
            return false;
        }

        private static object CreateObject(ConstructorInfo ctor, IDictionary<PropertyInfo, object> properties)
        {
            return ctor.Invoke(properties.Values.ToArray());
        }
    }
}