using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcCompositeWithComposites")]
    [XmlRoot(ElementName = "xmlCompositeWithComposites")]
    public class CompositeWithComposites
    {
        public static CompositeWithComposites Numbers
        {
            get
            {
                return new CompositeWithComposites
                    {
                        One = CompositeWithPrimitives.One,
                        Two = CompositeWithPrimitives.Two
                    };
            }
        }

        [DataMember(Name = "dcOne")]
        [XmlElement(ElementName = "xmlOne")]
        public CompositeWithPrimitives One { get; set; }

        [DataMember(Name = "dcTwo")]
        [XmlElement(ElementName = "xmlTwo")]
        public CompositeWithPrimitives Two { get; set; }
    }
}