using System;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestResult
    {
        public int IterationsCount { get; set; }

        public TimeSpan TotalTime { get; set; }

        public double Slowness { get; set; }
    }
}