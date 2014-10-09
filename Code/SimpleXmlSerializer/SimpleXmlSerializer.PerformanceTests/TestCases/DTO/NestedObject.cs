using System.Runtime.Serialization;

namespace SimpleXmlSerializer.PerformanceTests.TestCases.DTO
{
    [DataContract]
    public class NestedObject
    {
        [DataMember]
        public PlainObject Plain { get; set; }

        [DataMember]
        public NestedObject Nested { get; set; }
    }
}