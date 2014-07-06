using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests.Serialization
{
    [TestClass]
    public class SerializeComplexTypesTests
    {
        private const string AssetsDirectory = "Assets\\Complex";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Serialize_ComplexWithPrimitive()
        {
            SerializeAndAssert(Players.Xavi, "xavi");
        }

        [TestMethod]
        public void Serialize_ComplexWithNullable()
        {
            SerializeAndAssert(Coaches.Unknown, "unknownCoach");
        }

        [TestMethod]
        public void Serialize_ComplexWithCollection()
        {
            SerializeAndAssert(Teams.Barca, "barcaTeam");
        }

        [TestMethod]
        public void Serialize_ComplexWithComplex()
        {
            SerializeAndAssert(Clubs.Barca, "barcaClub");
        }

        private void SerializeAndAssert(object obj, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.SerializeAndAssertObject(obj, path);
        }
    }
}