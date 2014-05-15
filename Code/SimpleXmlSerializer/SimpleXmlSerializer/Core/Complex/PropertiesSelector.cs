using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core.Complex
{
    public class PropertiesSelector : IPropertiesSelector
    {
        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | 
                BindingFlags.SetProperty | BindingFlags.GetProperty);
        }
    }
}