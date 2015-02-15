using System;
using System.Linq;
using SimpleXmlSerializer.PerformanceTests.Core;
using SimpleXmlSerializer.PerformanceTests.TestCases;

namespace SimpleXmlSerializer.PerformanceTests.Cmd
{
    public class Program
    {
        private static readonly string sectionSeparator = string.Concat(Enumerable.Repeat("-", 50));

        public static void Main(string[] args)
        {
            var testSuite = new TestSuite
                {
                    TestCases = TestCasesFactory.CreateTestCases()
                };

            Console.WriteLine("Executing tests...");
            var testSuiteExecutor = new TestSuiteExecutor();
            var testSuiteResult = testSuiteExecutor.ExecuteTestSuite(testSuite);

            foreach (var item in testSuiteResult.TestCaseResults)
            {
                Console.WriteLine(item);
                Console.WriteLine(sectionSeparator);
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}