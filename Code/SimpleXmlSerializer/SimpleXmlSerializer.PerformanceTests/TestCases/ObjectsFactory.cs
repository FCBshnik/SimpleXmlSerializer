using System;
using System.Linq;
using SimpleXmlSerializer.PerformanceTests.TestCases.DTO;
using SimpleXmlSerializer.PerformanceTests.Utils;

namespace SimpleXmlSerializer.PerformanceTests.TestCases
{
    public static class ObjectsFactory
    {
        private static readonly Random random = new Random();

        public static PlainObject CreateRandomPlain()
        {
            return new PlainObject
                {
                    Bool = random.GetNextBool(),
                    DateTime = random.GetNextDateTime(),
                    Decimal = (decimal) random.NextDouble(),
                    Float = (float) random.NextDouble(),
                    Int = random.Next(),
                    String = Guid.NewGuid().ToString(),
                    TimeSpan = random.GetNextTimeSpan()
                };
        }

        public static CollectionsObject CreateRandomCollections(int elementsCount)
        {
            return new CollectionsObject
            {
                Array = Enumerable.Range(0, elementsCount).Select(i => Guid.NewGuid().ToString()).ToArray(),
                List = Enumerable.Range(0, elementsCount).Select(i => Guid.NewGuid().ToString()).ToList(),
                Dictionary = Enumerable.Range(0, elementsCount).ToDictionary(i => Guid.NewGuid().ToString(), i => Guid.NewGuid().ToString()),
            };
        }

        public static NestedObject CreateRandomNested(int nestingLevel)
        {
            return PopulateChildren(new NestedObject(), nestingLevel);
        }

        private static NestedObject PopulateChildren(NestedObject parent, int currentLevel)
        {
            parent.Plain = CreateRandomPlain();
            if (currentLevel > 0)
            {
                parent.Nested = PopulateChildren(new NestedObject(), currentLevel - 1);
            }

            return parent;
        }
    }
}