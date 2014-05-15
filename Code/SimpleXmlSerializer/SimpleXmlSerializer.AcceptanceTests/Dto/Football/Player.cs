using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Player
    {
        [XmlElement(ElementName = "shortName")]
        [DataMember(Name = "alias")]
        public string Name { get; set; }

        [XmlElement(ElementName = "shortNumber")]
        [DataMember(Name = "digit")]
        public int Number { get; set; }
    }
}