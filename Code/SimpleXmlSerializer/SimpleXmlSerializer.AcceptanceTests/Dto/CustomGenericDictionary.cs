using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [CollectionDataContract(Name = "cdcCustomDictionary", ItemName = "cdcAdd")]
    [DataContract(Name = "dcCustomDictionary")]
    [XmlRoot(ElementName = "xmlCustomDictionary")]
    public class CustomGenericDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public static CustomGenericDictionary<string, string> Numbers
        {
            get { return new CustomGenericDictionary<string, string> { { "One", "1" }, { "Two", "2" } }; }
        }
    }
}