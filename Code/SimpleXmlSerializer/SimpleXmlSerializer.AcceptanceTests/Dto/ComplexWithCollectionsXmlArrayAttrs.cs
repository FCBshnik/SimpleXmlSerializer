using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [XmlRoot(ElementName = "xmlComplexWithCollections")]
    public class ComplexWithCollectionsXmlArrayAttrs
    {
        public static ComplexWithCollectionsXmlArrayAttrs Numbers
        {
            get
            {
                return new ComplexWithCollectionsXmlArrayAttrs
                {
                    Collection1 = new List<string> { "One", "Two" },
                    Collection2 = new List<string> { "Three", "Four" },
                    Collection3 = new List<string> { "Five", "Six" },
                    Collection4= new List<string> { "Seven", "Eight" },
                };
            }
        }

        [XmlArray(ElementName = "xmlCollection")]
        [XmlArrayItem(ElementName = "xmlAdd")]
        public IEnumerable<string> Collection1 { get; set; }

        [XmlArray(ElementName = "xmlCollection")]
        public IEnumerable<string> Collection2 { get; set; }

        [XmlElement("xmlCollection")]
        [XmlArrayItem(ElementName = "xmlAdd")]
        public IEnumerable<string> Collection3 { get; set; }

        [XmlArrayItem(ElementName = "xmlAdd")]
        public IEnumerable<string> Collection4 { get; set; }
    }
}