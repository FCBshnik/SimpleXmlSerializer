using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Coach
    {
        [XmlElement(ElementName = "shortName")]
        [DataMember(Name= "alias")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "age")]
        [DataMember(Name = "numberOfYears")]
        public int? Age { get; set; }
    }
}