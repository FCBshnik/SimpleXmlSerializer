using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class CompositeTests : TestsBase
    {
        [TestMethod]
        public void CompositeWithPrimitives()
        {
            ActAndAssert(Dto.CompositeWithPrimitives.One, "compositeWithPrimitives");
        }

        [TestMethod]
        public void CompositeWithCollections()
        {
            ActAndAssert(Dto.CompositeWithCollections.Numbers, "compositeWithCollections");
        }

        [TestMethod]
        public void CompositeWithEmptyCollections()
        {
            ActAndAssert(Dto.CompositeWithCollections.Empties, "compositeWithEmptyCollections");
        }

        [TestMethod]
        public void CompositeWithComposites()
        {
            ActAndAssert(Dto.CompositeWithComposites.Numbers, "compositeWithComposites");
        }

        [TestMethod]
        public void CompositeWithNull()
        {
            var numbers = Dto.CompositeWithComposites.Numbers;
            numbers.One = null;

            ActAndAssert(numbers, "compositeWithNull");
        }
    }
}