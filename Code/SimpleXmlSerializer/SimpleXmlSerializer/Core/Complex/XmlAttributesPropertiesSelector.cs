using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesSelector"/> which 
    /// returns all read/write public instance properties of type
    /// based on <see cref="XmlElementAttribute"/>, <see cref="XmlArrayAttribute"/> 
    /// and <see cref="XmlIgnoreAttribute"/> attributes.
    /// </summary>
    public class XmlAttributesPropertiesSelector : IPropertiesSelector
    {
        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(pi => !pi.HasAttribute<XmlIgnoreAttribute>())
                .Select(p => new
                    {
                        Property = p,
                        XmlElementAttr = p.FindAttribute<XmlElementAttribute>(),
                        XmlArrayAttr = p.FindAttribute<XmlArrayAttribute>(),
                    })
                .Where(p => p.XmlArrayAttr != null || p.XmlElementAttr != null)
                .OrderBy(p => p.XmlArrayAttr != null ? p.XmlArrayAttr.Order : p.XmlElementAttr.Order)
                .Select(p => p.Property);
        }
    }
}