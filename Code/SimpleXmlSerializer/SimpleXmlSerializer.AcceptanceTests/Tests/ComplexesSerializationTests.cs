using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class ComplexesSerializationTests
    {
        private const string AssetsDirectory = "Assets\\Complex";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Serialize_ComplexWithPrimitives()
        {
            SerializeAndAssert(Players.Xavi, "complexWithPrimitives");
        }

        [TestMethod]
        public void Serialize_ComplexWithNullables()
        {
            SerializeAndAssert(Coaches.Unknown, "complexWithNullables");
        }

        [TestMethod]
        public void Serialize_ComplexWithCollections()
        {
            SerializeAndAssert(Teams.Barca, "complexWithCollections");
        }

        [TestMethod]
        public void Serialize_ComplexWithComplexes()
        {
            SerializeAndAssert(Clubs.Barca, "complexWithComplexes");
        }

        private void SerializeAndAssert(object obj, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.SerializeAndAssertObject(obj, path);
        }
    }
}