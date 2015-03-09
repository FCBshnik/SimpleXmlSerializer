using System;
using System.Reflection;
using System.Xml.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    public class XmlAttributesNameProvider : INameProvider
    {
        public NodeName GetNodeName(Type type)
        {
            var xmlRootAttr = type.FindAttribute<XmlRootAttribute>();
            if (xmlRootAttr != null)
            {
                return new NodeName(xmlRootAttr.ElementName);
            }

            return NodeName.Empty;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var elementName = string.Empty;
            var attributeName = string.Empty;
            var itemName = string.Empty;

            var xmlElementAttr = propertyInfo.FindAttribute<XmlElementAttribute>();
            if (xmlElementAttr != null)
            {
                elementName = xmlElementAttr.ElementName;
            }

            var xmlArrayAttr = propertyInfo.FindAttribute<XmlArrayAttribute>();
            if (xmlArrayAttr != null)
            {
                elementName = xmlArrayAttr.ElementName;
            }

            var xmlArrayItemAttr = propertyInfo.FindAttribute<XmlArrayItemAttribute>();
            if (xmlArrayItemAttr != null)
            {
                itemName = xmlArrayItemAttr.ElementName;
            }

            var xmlAttributeAttr = propertyInfo.FindAttribute<XmlAttributeAttribute>();
            if (xmlAttributeAttr != null)
            {
                attributeName = xmlAttributeAttr.AttributeName;
            }

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}