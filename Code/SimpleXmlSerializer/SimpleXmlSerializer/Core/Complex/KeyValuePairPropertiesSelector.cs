using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    internal class KeyValuePairPropertiesSelector : IPropertiesSelector
    {
        private readonly IPropertiesSelector defaultSelector;
        private readonly IPropertiesSelector selector = new PublicPropertiesSelector();

        public KeyValuePairPropertiesSelector(IPropertiesSelector defaultSelector)
        {
            this.defaultSelector = defaultSelector;
        }

        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                return selector.SelectProperties(type);
            }

            return defaultSelector.SelectProperties(type);
        }
    }
}