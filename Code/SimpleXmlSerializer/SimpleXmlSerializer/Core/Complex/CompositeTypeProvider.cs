using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="ICompositeTypeProvider"/> which uses 
    /// specified <see cref="IPropertiesSelector"/> to get properties of composite object
    /// and parameterless constructor (if exists) to create instances of composite types.
    /// </summary>
    public class CompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IPropertiesSelector propertiesSelector;

        public CompositeTypeProvider(IPropertiesSelector propertiesSelector)
        {
            if (propertiesSelector == null) 
                throw new ArgumentNullException("propertiesSelector");

            this.propertiesSelector = propertiesSelector;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            var properties = propertiesSelector.SelectProperties(type).ToList();
            var getters = properties.ToDictionary(p => p, ExpressionUtils.GetPropertyGetter);

            // special handling for KeyValuePair
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                // todo: use compiled ctor
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);

                description = new CompositeTypeDescription(properties, ps => CreateObject(ctor, ps), (obj, pi) => getters[pi](obj));
                return true;
            }

            var ctorFunc = ExpressionUtils.GetFactory(type);
            description = new CompositeTypeDescription(properties, ps => CreateObject(ctorFunc, ps), (obj, pi) => getters[pi](obj));
            return true;
        }

        private static object CreateObject(Func<object> ctor, IDictionary<PropertyInfo, object> properties)
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