using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    [DataContract(Name = "dcComplexWithCollections")]
    [XmlRoot(ElementName = "xmlComplexWithCollections")]
    public class ComplexWithCollections
    {
        public static ComplexWithCollections Numbers
        {
            get
            {
                return new ComplexWithCollections
                    {
                        Collection = new List<string> {"One", "Two"},
                        Dictionary = new Dictionary<string, string> {{"One", "1"}, {"Two", "2"}}
                    };
            }
        }

        public static ComplexWithCollections Empties
        {
            get
            {
                return new ComplexWithCollections
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