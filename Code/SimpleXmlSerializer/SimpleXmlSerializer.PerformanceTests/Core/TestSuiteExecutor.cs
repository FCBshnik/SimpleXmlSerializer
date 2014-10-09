using System.Linq;

namespace SimpleXmlSerializer.PerformanceTests.Core
{
    public class TestSuiteExecutor
    {
        public TestSuiteResult ExecuteTestSuite(TestSuite testSuite)
        {
            var testCaseExecutor = new TestCaseExecutor();
            return new TestSuiteResult
                {
                    TestCaseResults = testSuite.TestCases.Select(testCaseExecutor.ExecuteTestCase).ToList()
                };
        }
    }
}