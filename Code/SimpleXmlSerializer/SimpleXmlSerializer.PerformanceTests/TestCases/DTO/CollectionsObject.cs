using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.PerformanceTests.TestCases.DTO
{
    [DataContract]
    public class CollectionsObject
    {
        [DataMember]
        public string[] Array { get; set; }

        [DataMember]
        public List<string> List { get; set; }

        [DataMember]
        public Dictionary<string, string> Dictionary { get; set; }
    }
}