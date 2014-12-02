using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    // hack to not skip KeyValuePair properties which do not have serialization attributes
    internal class SpecialPropertiesSelector : IPropertiesSelector
    {
        private readonly IPropertiesSelector defaultSelector;
        private readonly IPropertiesSelector selector;

        public SpecialPropertiesSelector(IPropertiesSelector defaultSelector, IPropertiesSelector selector)
        {
            this.defaultSelector = defaultSelector;
            this.selector = selector;
        }

        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                return defaultSelector.SelectProperties(type);
            }

            return selector.SelectProperties(type);
        }
    }
}