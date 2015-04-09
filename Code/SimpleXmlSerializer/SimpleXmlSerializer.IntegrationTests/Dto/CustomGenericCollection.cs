using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.IntegrationTests.Dto
{
    [CollectionDataContract(Name = "cdcCustomCollection", ItemName = "cdcAdd")]
    [DataContract(Name = "dcCustomCollection")]
    [XmlRoot(ElementName = "xmlCustomCollection")]
    public class CustomGenericCollection<T> : List<T>
    {
        public static CustomGenericCollection<string> Numbers
        {
            get { return new CustomGenericCollection<string> { "One", "Two" }; }
        }
    }
}