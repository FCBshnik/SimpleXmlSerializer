using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithPrimitives")]
    [XmlRoot(ElementName = "xmlComplexWithPrimitives")]
    public class ComplexWithPrimitivesOrder
    {
        public static ComplexWithPrimitivesOrder One
        {
            get
            {
                return new ComplexWithPrimitivesOrder { String = "One", Int = 1 };
            }
        }

        [DataMember(Name = "dcString", Order = 2)]
        [XmlElement(ElementName = "xmlString", Order = 2)]
        public string String { get; set; }

        [DataMember(Name = "dcInt", Order = 1)]
        [XmlElement(ElementName = "xmlInt", Order = 1)]
        public int Int { get; set; }
    }

}