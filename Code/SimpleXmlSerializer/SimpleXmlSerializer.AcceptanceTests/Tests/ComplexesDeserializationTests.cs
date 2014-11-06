using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexesDeserializationTests
    {
        private const string AssetsDirectory = "Assets\\Complex";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Deserialize_ComplexWithPrimitives()
        {
            DeserializeAndAssert(Players.Xavi, "complexWithPrimitives");
        }

        [TestMethod]
        public void Deserialize_ComlexWithNullables()
        {
            DeserializeAndAssert(Coaches.Unknown, "complexWithNullables");
        }

        [TestMethod]
        public void Deserialize_ComplexWithCollections()
        {
            DeserializeAndAssert(Teams.Barca, "complexWithCollections");
        }

        [TestMethod]
        public void Deserialize_ComplexWithComplexes()
        {
            DeserializeAndAssert(Clubs.Barca, "complexWithComplexes");
        }

        private void DeserializeAndAssert(object expected, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}