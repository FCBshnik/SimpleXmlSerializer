using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    [XmlRoot(ElementName = "footballClub")]
    [DataContract(Name = "soccerClub")]
    public class Club
    {
        [XmlElement(ElementName = "shortName")]
        [DataMember(Name = "alias")]
        public string Name { get; set; }

        [XmlElement(ElementName = "players")]
        [DataMember(Name = "stuff")]
        public Team Team { get; set; }

        [XmlElement(ElementName = "headCoach")]
        [DataMember(Name = "trainer")]
        public Coach Coach { get; set; }

        public DateTime Founded { get; set; }

        [XmlIgnore]
        [IgnoreDataMember]
        public string President { get; set; }
    }
}