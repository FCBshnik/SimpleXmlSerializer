using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithPrimitives")]
    [XmlRoot(ElementName = "xmlComplexWithPrimitives")]
    public class ComplexWithPrimitives
    {
        public static readonly ComplexWithPrimitives One = new ComplexWithPrimitives { String = "One", Int = 1 };
        public static readonly ComplexWithPrimitives Two = new ComplexWithPrimitives { String = "Two", Int = 2 };

        [DataMember(Name = "dcString")]
        [XmlElement(ElementName = "xmlString")]
        public string String { get; set; }

        [DataMember(Name = "dcInt")]
        [XmlElement(ElementName = "xmlInt")]
        public int Int { get; set; }
    }
}