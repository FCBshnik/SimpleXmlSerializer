using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithComplexes")]
    [XmlRoot(ElementName = "xmlComplexWithComplexes")]
    public class ComplexWithComplexes
    {
        public static readonly ComplexWithComplexes Numbers = new ComplexWithComplexes
            {
                One = ComplexWithPrimitives.One, Two = ComplexWithPrimitives.Two
            };

        [DataMember(Name = "dcOne")]
        [XmlElement(ElementName = "xmlOne")]
        public ComplexWithPrimitives One { get; set; }

        [DataMember(Name = "dcTwo")]
        [XmlElement(ElementName = "xmlTwo")]
        public ComplexWithPrimitives Two { get; set; }
    }
}