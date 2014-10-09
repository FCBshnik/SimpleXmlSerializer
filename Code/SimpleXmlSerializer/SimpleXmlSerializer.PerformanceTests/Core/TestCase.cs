using System.Collections.Generic;
using SimpleXmlSerializer.PerformanceTests.Adapters;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestCase
    {
        public string Name { get; set; }

        public IList<IXmlSerializerAdapter> Serializers { get; set; }

        public object ObjectToSerialize { get; set; }

        public string StringToDeserialize { get; set; }

        public int IterationsCount { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1} iterations)", Name, IterationsCount);
        }
    }
}