using System;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.PerformanceTests.TestCases.DTO
{
    [DataContract]
    public class PlainObject
    {
        [DataMember]
        public string String { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; }

        [DataMember]
        public int Int { get; set; }

        [DataMember]
        public bool Bool { get; set; }

        [DataMember]
        public decimal Decimal { get; set; }

        [DataMember]
        public float Float { get; set; }

        [DataMember]
        public TimeSpan TimeSpan { get; set; }
    }
}