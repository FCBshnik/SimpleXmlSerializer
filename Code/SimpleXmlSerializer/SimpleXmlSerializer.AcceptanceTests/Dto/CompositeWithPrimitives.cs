using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    [DataContract(Name = "dcCompositeWithPrimitives")]
    [XmlRoot(ElementName = "xmlCompositeWithPrimitives")]
    public class CompositeWithPrimitives
    {
        public static CompositeWithPrimitives One
        {
            get
            {
                return new CompositeWithPrimitives { String = "One", Int = 1 };
            }
        }

        public static CompositeWithPrimitives Two
        {
            get
            {
                return new CompositeWithPrimitives { String = "Two", Int = 2 };
            }
        }

        [DataMember(Name = "dcString")]
        [XmlElement(ElementName = "xmlString")]
        public string String { get; set; }

        [DataMember(Name = "dcInt")]
        [XmlElement(ElementName = "xmlInt")]
        public int Int { get; set; }
    }
}