using System;
using System.Reflection;
using System.Xml.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core.Naming
{
    public class XmlAttributesNameProvider : INameProvider
    {
        public NodeName GetNodeName(Type type)
        {
            var xmlRootAttr = type.FindAttribute<XmlRootAttribute>();
            if (xmlRootAttr != null)
            {
                return new NodeName(xmlRootAttr.ElementName, string.Empty);
            }

            return NodeName.Empty;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var xmlElementAttr = propertyInfo.FindAttribute<XmlElementAttribute>();
            if (xmlElementAttr != null)
            {
                return new NodeName(xmlElementAttr.ElementName);
            }

            var xmlArrayAttr = propertyInfo.FindAttribute<XmlArrayAttribute>();
            var xmlArrayItemAttr = propertyInfo.FindAttribute<XmlArrayItemAttribute>();
            if (xmlArrayAttr != null)
            {
                var itemName = xmlArrayItemAttr != null ? xmlArrayItemAttr.ElementName : string.Empty;
                return new NodeName(xmlArrayAttr.ElementName, itemName);
            }

            var xmlAttributeAttr = propertyInfo.FindAttribute<XmlAttributeAttribute>();
            if (xmlAttributeAttr != null)
            {
                return new NodeName(string.Empty, string.Empty, xmlAttributeAttr.AttributeName);
            }

            return NodeName.Empty;
        }
    }
}