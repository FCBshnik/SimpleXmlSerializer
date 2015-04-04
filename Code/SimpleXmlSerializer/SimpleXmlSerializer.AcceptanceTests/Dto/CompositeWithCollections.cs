using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcCompositeWithCollections")]
    [XmlRoot(ElementName = "xmlCompositeWithCollections")]
    public class CompositeWithCollections
    {
        public static CompositeWithCollections Numbers
        {
            get
            {
                return new CompositeWithCollections
                    {
                        Collection = new List<string> {"One", "Two"},
                        Dictionary = new Dictionary<string, string> {{"One", "1"}, {"Two", "2"}}
                    };
            }
        }

        public static CompositeWithCollections Empties
        {
            get
            {
                return new CompositeWithCollections
                {
                    Collection = new List<string>(),
                    Dictionary = new Dictionary<string, string>()
                };
            }
        }

        [DataMember(Name = "dcCollection")]
        [XmlElement(ElementName = "xmlCollection")]
        public IEnumerable<string> Collection { get; set; }

        [DataMember(Name = "dcDictionary")]
        [XmlElement(ElementName = "xmlDictionary")]
        public IDictionary<string, string> Dictionary { get; set; }
    }
}