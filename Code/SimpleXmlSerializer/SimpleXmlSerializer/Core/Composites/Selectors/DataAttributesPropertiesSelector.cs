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
    /// based on <see cref="DataContractAttribute"/>, <see cref="DataMemberAttribute"/> and <see cref="IgnoreDataMemberAttribute"/> attributes.
    /// </summary>
    public class DataAttributesPropertiesSelector : IPropertiesSelector
    {
        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            if (!type.HasAttribute<DataContractAttribute>())
            {
                throw new SerializationException(string.Format("Type '{0}' is not marked by DataContractAttribute attribute", type));
            }

            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(pi => !pi.HasAttribute<IgnoreDataMemberAttribute>())
                .Select(pi => new
                    {
                        Property = pi, DataMemberAttr = pi.FindAttribute<DataMemberAttribute>()
                    })
                .Where(p => p.DataMemberAttr != null)
                .OrderBy(p => p.DataMemberAttr.Order)
                .Select(p => p.Property);
        }
    }
}