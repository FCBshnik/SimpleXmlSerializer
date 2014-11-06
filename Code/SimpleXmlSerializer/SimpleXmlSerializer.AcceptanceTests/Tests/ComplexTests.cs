using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexTests : TestsBase
    {
        [TestMethod]
        public void ComplexWithPrimitives()
        {
            ActAndAssert(Players.Xavi, "complexWithPrimitives");
        }

        [TestMethod]
        public void ComplexWithNullables()
        {
            ActAndAssert(Coaches.Unknown, "complexWithNullables");
        }

        [TestMethod]
        public void ComplexWithCollections()
        {
            ActAndAssert(Teams.Barca, "complexWithCollections");
        }

        [TestMethod]
        public void ComplexWithComplexes()
        {
            ActAndAssert(Clubs.Barca, "complexWithComplexes");
        }
    }
}