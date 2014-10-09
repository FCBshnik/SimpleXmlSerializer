using System.Collections.Generic;
using System.Linq;
using SimpleXmlSerializer.PerformanceTests.Adapters;
using SimpleXmlSerializer.PerformanceTests.Core;

namespace SimpleXmlSerializer.PerformanceTests.TestCases
{
    public static class TestCasesFactory
    {
        public static IEnumerable<TestCase> CreateTestCases()
        {
            yield return PlainObject(10000);
            yield return Collections(100, 10000);
            yield return Collections(1000, 100);
            yield return Nested(10, 10000);
        }

        private static TestCase PlainObject(int iterationsCount)
        {
            var objectToSerialize = ObjectsFactory.CreateRandomPlain();
            return new TestCase
                {
                    Name = "Plain object with primitive properties",
                    ObjectToSerialize = objectToSerialize,
                    Serializers = XmlSerializersFactory.CreateSerializers(objectToSerialize.GetType()).ToList(),
                    IterationsCount = iterationsCount
                };
        }

        private static TestCase Collections(int elementsCount, int iterationsCount)
        {
            var objectToSerialize = ObjectsFactory.CreateRandomCollections(elementsCount);

            return new TestCase
            {
                Name = string.Format("Object with collections with {0} elements", elementsCount),
                ObjectToSerialize = objectToSerialize,
                Serializers = XmlSerializersFactory.CreateSerializers(objectToSerialize.GetType()).ToList(),
                IterationsCount = iterationsCount
            };
        }

        private static TestCase Nested(int nestingLevel, int iterationsCount)
        {
            var objectToSerialize = ObjectsFactory.CreateRandomNested(nestingLevel);

            return new TestCase
            {
                Name = string.Format("Nested object with nesting level {0}", nestingLevel),
                ObjectToSerialize = objectToSerialize,
                Serializers = XmlSerializersFactory.CreateSerializers(objectToSerialize.GetType()).ToList(),
                IterationsCount = iterationsCount
            };
        }
    }
}