using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    [CollectionDataContract(Name = "cdcCustomCollection", ItemName = "cdcAdd")]
    [DataContract(Name = "dcCustomCollection")]
    [XmlRoot(ElementName = "xmlCustomCollection")]
    public class CustomCollection : List<string>
    {
        public static CustomCollection Numbers
        {
            get { return new CustomCollection { "One", "Two" }; }
        }
    }
}