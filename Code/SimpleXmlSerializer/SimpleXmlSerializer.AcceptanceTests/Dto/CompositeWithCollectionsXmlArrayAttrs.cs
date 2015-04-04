using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [XmlRoot(ElementName = "xmlCompositeWithCollections")]
    public class CompositeWithCollectionsXmlArrayAttrs
    {
        public static CompositeWithCollectionsXmlArrayAttrs Numbers
        {
            get
            {
                return new CompositeWithCollectionsXmlArrayAttrs
                {
                    Collection1 = new List<string> { "One", "Two" },
                    Collection2 = new List<string> { "Three", "Four" },
                    Collection3 = new List<string> { "Five", "Six" },
                };
            }
        }

        [XmlArray(ElementName = "xmlCollection1")]
        [XmlArrayItem(ElementName = "xmlAdd")]
        public IEnumerable<string> Collection1 { get; set; }

        [XmlArray(ElementName = "xmlCollection2")]
        public IEnumerable<string> Collection2 { get; set; }

        [XmlElement("xmlCollection3")]
        [XmlArrayItem(ElementName = "xmlAdd")]
        public IEnumerable<string> Collection3 { get; set; }
    }
}