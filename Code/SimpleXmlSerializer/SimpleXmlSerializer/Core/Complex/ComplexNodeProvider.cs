using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class ComplexNodeProvider : IComplexNodeProvider
    {
        private readonly IPropertiesSelector propertiesSelector;

        public ComplexNodeProvider(IPropertiesSelector propertiesSelector)
        {
            this.propertiesSelector = propertiesSelector;
        }

        public ComplexNodeDescription GetDescription(Type type)
        {
            var properties = propertiesSelector.SelectProperties(type).ToList();
            var getters = properties.ToDictionary(p => p, ExpressionUtils.GetPropertyGetter);

            // todo: use compiled ctor
            // todo: handle all parameterless types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);
                if (ctor == null)
                {
                    throw new SerializationException();
                }

                return new ComplexNodeDescription(properties, ps => CreateKeyValuePair(ctor, ps))
                    {
                        Getter = (obj, pi) => getters[pi](obj)
                    };
            }

            var ctorFunc = ExpressionUtils.GetFactory(type);
            return new ComplexNodeDescription(properties, ps => CreateObject(ps, ctorFunc))
                {
                    Getter = (obj, pi) => getters[pi](obj)
                };
        }

        private static object CreateObject(IEnumerable<KeyValuePair<PropertyInfo, object>> properties, Func<object> ctor)
        {
            var value = ctor();
            foreach (var pair in properties)
            {
                pair.Key.SetValue(value, pair.Value, null);
            }

            return value;
        }

        private static object CreateKeyValuePair(ConstructorInfo ctor, IEnumerable<KeyValuePair<PropertyInfo, object>> properties)
        {
            return ctor.Invoke(properties.Select(p => p.Value).ToArray());
        }
    }
}