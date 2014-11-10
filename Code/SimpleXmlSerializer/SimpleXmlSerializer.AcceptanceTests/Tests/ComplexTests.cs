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
        public void ComplexWithComplexes()
        {
            ActAndAssert(Dto.ComplexWithComplexes.Numbers, "complexWithComplexes");
        }
    }
}