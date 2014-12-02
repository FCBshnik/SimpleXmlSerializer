using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithCustomCollections")]
    [XmlRoot(ElementName = "xmlComplexWithCustomCollections")]
    public class ComplexWithCustomCollections
    {
        public static ComplexWithCustomCollections Numbers
        {
            get
            {
                return new ComplexWithCustomCollections
                {
                    Collection = CustomCollection.Numbers,
                    Dictionary = CustomDictionary.Numbers
                };
            }
        }

        [DataMember(Name = "dcCollection")]
        [XmlElement(ElementName = "xmlCollection")]
        public CustomCollection Collection { get; set; }

        [DataMember(Name = "dcDictionary")]
        [XmlElement(ElementName = "xmlDictionary")]
        public CustomDictionary Dictionary { get; set; }
    }
}