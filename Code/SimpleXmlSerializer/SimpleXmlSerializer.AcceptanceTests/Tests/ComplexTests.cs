using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexTests
    {
        private const string AssetsDirectory = "Assets\\Complex";

        private static readonly XmlSerializer serializer = new XmlSerializer();

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

        private static void ActAndAssert(object obj, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");
            serializer.SerializeAndAssert(obj, path);
            serializer.DeserializeAndAssert(obj, path);
        }
    }
}