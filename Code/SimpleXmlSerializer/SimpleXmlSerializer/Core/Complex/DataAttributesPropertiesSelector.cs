using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesSelector"/> which 
    /// returns all read/write public instance properties of type
    /// accordingly with data contract attributes.
    /// </summary>
    public class DataAttributesPropertiesSelector : IPropertiesSelector
    {
        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public |
                BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(pi => !pi.HasAttribute<IgnoreDataMemberAttribute>() && pi.HasAttribute<DataMemberAttribute>());
        }
    }
}