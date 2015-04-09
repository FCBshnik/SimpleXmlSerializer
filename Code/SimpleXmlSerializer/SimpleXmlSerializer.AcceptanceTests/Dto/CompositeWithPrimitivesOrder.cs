using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    [DataContract(Name = "dcCompositeWithPrimitives")]
    [XmlRoot(ElementName = "xmlCompositeWithPrimitives")]
    public class CompositeWithPrimitivesOrder
    {
        public static CompositeWithPrimitivesOrder One
        {
            get
            {
                return new CompositeWithPrimitivesOrder { String = "One", Int = 1 };
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