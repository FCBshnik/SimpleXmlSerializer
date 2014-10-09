using System;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestResult
    {
        public int IterationsCount { get; set; }

        public TimeSpan TotalTime { get; set; }

        public override string ToString()
        {
            return string.Format("Total: {0}", TotalTime);
        }
    }
}