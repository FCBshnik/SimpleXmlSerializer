using System;
using System.Linq;
using SimpleXmlSerializer.PerformanceTests.Core;
using SimpleXmlSerializer.PerformanceTests.TestCases;

namespace SimpleXmlSerializer.PerformanceTests.Cmd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var testSuite = new TestSuite
                {
                    TestCases = TestCasesFactory.CreateTestCases()
                };

            var testSuiteExecutor = new TestSuiteExecutor();
            var testSuiteResult = testSuiteExecutor.ExecuteTestSuite(testSuite);

            foreach (var item in testSuiteResult.TestCaseResults)
            {
                Console.WriteLine(item);
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}