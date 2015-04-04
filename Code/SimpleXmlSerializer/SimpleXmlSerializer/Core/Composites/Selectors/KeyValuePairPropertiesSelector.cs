using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to always return Key and Value properties for KeyValuePair type.
    /// For not KeyValuePair types returns empty list.
    /// </summary>
    internal class KeyValuePairPropertiesSelector : IPropertiesSelector
    {
        private readonly IPropertiesSelector selector = new PublicPropertiesSelector();

        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                return selector.SelectProperties(type);
            }

            return Enumerable.Empty<PropertyInfo>();
        }
    }
}