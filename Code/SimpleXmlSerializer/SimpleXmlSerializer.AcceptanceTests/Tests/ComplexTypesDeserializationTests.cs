using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexTypesDeserializationTests
    {
        private const string AssetsDirectory = "Assets\\Complex";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Deserialize_ComlexWithPrimitive()
        {
            DeserializeAndAssert(Players.Xavi, "xavi");
        }

        [TestMethod]
        public void Deserialize_ComlexWithNullable()
        {
            DeserializeAndAssert(Coaches.Unknown, "unknownCoach");
        }

        [TestMethod]
        public void Deserialize_ComplexWithCollection()
        {
            DeserializeAndAssert(Teams.Barca, "barcaTeam");
        }

        [TestMethod]
        public void Deserialize_ComplexWithComplex()
        {
            DeserializeAndAssert(Clubs.Barca, "barcaClub");
        }

        private void DeserializeAndAssert(object expected, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}