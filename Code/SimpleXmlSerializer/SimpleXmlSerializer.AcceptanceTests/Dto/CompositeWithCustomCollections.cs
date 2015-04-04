using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcCompositeWithCustomCollections")]
    [XmlRoot(ElementName = "xmlCompositeWithCustomCollections")]
    public class CompositeWithCustomCollections
    {
        public static CompositeWithCustomCollections Numbers
        {
            get
            {
                return new CompositeWithCustomCollections
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