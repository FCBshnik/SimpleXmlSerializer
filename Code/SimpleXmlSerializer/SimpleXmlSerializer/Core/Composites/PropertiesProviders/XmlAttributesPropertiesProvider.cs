using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesProvider"/> which 
    /// returns all read/write public instance properties of type
    /// based on <see cref="XmlElementAttribute"/>, <see cref="XmlArrayAttribute"/> 
    /// and <see cref="XmlIgnoreAttribute"/> attributes.
    /// </summary>
    public class XmlAttributesPropertiesProvider : IPropertiesProvider
    {
        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(pi => !pi.HasAttribute<XmlIgnoreAttribute>())
                .Select(p => new
                    {
                        Property = p,
                        XmlElementAttr = p.FindAttribute<XmlElementAttribute>(),
                        XmlArrayAttr = p.FindAttribute<XmlArrayAttribute>(),
                        XmlAttributeAttr = p.FindAttribute<XmlAttributeAttribute>()
                    })
                .Where(p => p.XmlArrayAttr != null || p.XmlElementAttr != null || p.XmlAttributeAttr != null)
                .Select(p => new
                {
                    Property = p.Property,
                    Order = p.XmlArrayAttr != null ? p.XmlArrayAttr.Order : (p.XmlElementAttr != null ? p.XmlElementAttr.Order : -1)
                })
                .OrderBy(p => p.Order)
                .Select(p => p.Property);
        }
    }
}