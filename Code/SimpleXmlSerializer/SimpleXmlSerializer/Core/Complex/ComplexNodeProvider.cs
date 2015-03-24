using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            // special handling for KeyValuePair
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                // todo: use compiled ctor
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);

                return new ComplexNodeDescription(properties, ps => CreateObject(ctor, ps))
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

        private static object CreateObject(IDictionary<PropertyInfo, object> properties, Func<object> ctor)
        {
            var value = ctor();
            foreach (var propertyInfo in properties.Keys)
            {
                propertyInfo.SetValue(value, properties[propertyInfo], null);
            }

            return value;
        }

        private static object CreateObject(ConstructorInfo ctor, IDictionary<PropertyInfo, object> properties)
        {
            return ctor.Invoke(properties.Values.ToArray());
        }
    }
}