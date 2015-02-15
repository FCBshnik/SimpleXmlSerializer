using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesSelector"/> which returns all writable public instance properties of type.
    /// </summary>
    public class PublicPropertiesSelector : IPropertiesSelector
    {
        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | 
                BindingFlags.SetProperty | BindingFlags.GetProperty);
        }
    }
}