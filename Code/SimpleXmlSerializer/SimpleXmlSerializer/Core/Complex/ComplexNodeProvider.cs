using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            var propertiesInfo = propertiesSelector.SelectProperties(type).ToList();
            return new ComplexNodeDescription(propertiesInfo, ps => Create(ps, type));
        }

        private object Create(IDictionary<PropertyInfo, object> properties, Type type)
        {
            var value = Activator.CreateInstance(type);

            var ctorProperties = properties.Keys.Where(pi => !pi.CanWrite).ToList();
            if (ctorProperties.Any())
            {
                var ctorInfo = type.GetConstructor(ctorProperties.Select(pi => pi.PropertyType).ToArray());
                if (ctorInfo != null)
                {
                    value = ctorInfo.Invoke(ctorProperties.Select(pi => properties[pi]).ToArray());
                }
            }

            foreach (var pair in properties.Where(p => !ctorProperties.Contains(p.Key)))
            {
                pair.Key.SetValue(value, pair.Value, null);
            }

            return value;
        }
    }
}