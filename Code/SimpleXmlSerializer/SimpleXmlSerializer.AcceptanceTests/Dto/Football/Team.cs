using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Team
    {
        [XmlArray(ElementName = "bestMidfielders")]
        [XmlArrayItem(ElementName = "midfielder")]
        [DataMember(Name = "players")]
        public IEnumerable<Player> Midfielders { get; set; }
    }
}