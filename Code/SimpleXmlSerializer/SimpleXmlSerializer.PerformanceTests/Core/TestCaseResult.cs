using System.Collections.Generic;
using System.Text;
using SimpleXmlSerializer.PerformanceTests.Adapters;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestCaseResult
    {
        public TestCaseResult()
        {
            SerializersResults = new Dictionary<IXmlSerializerAdapter, TestResult>();
        }

        public TestCase TestCase { get; set; }

        public Dictionary<IXmlSerializerAdapter, TestResult> SerializersResults { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("TestCase: {0}", TestCase);
            foreach (var result in SerializersResults)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("{0,-13} {1,-11} {2:0.###}", result.Key.Name, result.Value.TotalTime, result.Value.Slowness);
            }

            return stringBuilder.ToString();
        }
    }
}