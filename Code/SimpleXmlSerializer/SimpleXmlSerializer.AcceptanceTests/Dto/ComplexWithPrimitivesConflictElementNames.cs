using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithPrimitives")]
    [XmlRoot(ElementName = "xmlComplexWithPrimitives")]
    public class ComplexWithPrimitivesConflictElementNames
    {
        public static ComplexWithPrimitivesConflictElementNames One
        {
            get
            {
                return new ComplexWithPrimitivesConflictElementNames { String = "One", Int = 1 };
            }
        }

        [DataMember(Name = "dcElementName")]
        [XmlElement(ElementName = "xmlElementName")]
        public string String { get; set; }

        [DataMember(Name = "dcElementName")]
        [XmlElement(ElementName = "xmlElementName")]
        public int Int { get; set; }
    }

}