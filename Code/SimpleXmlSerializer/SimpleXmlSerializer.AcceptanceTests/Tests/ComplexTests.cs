using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexTests : TestsBase
    {
        [TestMethod]
        public void ComplexWithPrimitives()
        {
            ActAndAssert(Dto.ComplexWithPrimitives.One, "complexWithPrimitives");
        }

        [TestMethod]
        public void ComplexWithCollections()
        {
            ActAndAssert(Dto.ComplexWithCollections.Numbers, "complexWithCollections");
        }

        [TestMethod]
        public void ComplexWithEmptyCollections()
        {
            ActAndAssert(Dto.ComplexWithCollections.Empties, "complexWithEmptyCollections");
        }

        [TestMethod]
        public void ComplexWithComplexes()
        {
            ActAndAssert(Dto.ComplexWithComplexes.Numbers, "complexWithComplexes");
        }

        [TestMethod]
        public void ComplexWithNull()
        {
            var numbers = Dto.ComplexWithComplexes.Numbers;
            numbers.One = null;

            ActAndAssert(numbers, "complexWithNull");
        }
    }
}