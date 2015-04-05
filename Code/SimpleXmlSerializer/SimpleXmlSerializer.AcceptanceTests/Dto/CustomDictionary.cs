using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [CollectionDataContract(Name = "cdcCustomDictionary", ItemName = "cdcAdd", KeyName = "cdcKey", ValueName = "cdcValue")]
    [DataContract(Name = "dcCustomDictionary")]
    [XmlRoot(ElementName = "xmlCustomDictionary")]
    public class CustomDictionary : Dictionary<string, string>
    {
        public static CustomDictionary Numbers
        {
            get { return new CustomDictionary { {"One", "1"}, {"Two", "2"} }; }
        }
    }
}