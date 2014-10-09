using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SimpleXmlSerializer.PerformanceTests.Utils;
using System.Linq;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestCaseExecutor
    {
        public TestCaseResult ExecuteTestCase(TestCase testCase)
        {
            var result = new TestCaseResult { TestCase = testCase };
            var stopwatch = new Stopwatch();
            
            foreach (var serializer in testCase.Serializers)
            {
                var times = new List<TimeSpan>();
                for (var i = 0; i < testCase.IterationsCount; i++)
                {
                    using (var outputStream = new MemoryStream())
                    {
                        stopwatch.Restart();
                        serializer.Serialize(testCase.ObjectToSerialize, outputStream);
                        stopwatch.Stop();
                        times.Add(stopwatch.Elapsed);
                    }
                }

                var serializerResult = new TestResult
                    {
                        IterationsCount = times.Count,
                        TotalTime = times.Sum()
                    };
                result.SerializersResults[serializer] = serializerResult;
            }

            result.SerializersResults = result.SerializersResults
                .OrderBy(r => r.Value.TotalTime)
                .ToDictionary();
            return result;
        }
    }
}