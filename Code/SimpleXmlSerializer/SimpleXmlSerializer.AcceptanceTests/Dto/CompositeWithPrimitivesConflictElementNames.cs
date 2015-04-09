using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    [DataContract(Name = "dcCompositeWithPrimitives")]
    [XmlRoot(ElementName = "xmlCompositeWithPrimitives")]
    public class CompositeWithPrimitivesConflictElementNames
    {
        public static CompositeWithPrimitivesConflictElementNames One
        {
            get
            {
                return new CompositeWithPrimitivesConflictElementNames { String = "One", Int = 1 };
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