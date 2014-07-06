using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests.NameProviders
{
    [TestClass]
    public class NameProvidersTests
    {
        private const string AssetsDirectory = "Assets\\NameProviders";

        [TestMethod]
        public void Serialize_DefaultNameProvider()
        {
            var serializer = new XmlSerializer();

            SerializeAndAssert(serializer, Clubs.Barca, "barcaClub_default");
        }

        [TestMethod]
        public void Deserialize_DefaultNameProvider()
        {
            var serializer = new XmlSerializer();

            DeserializeAndAssert(serializer, Clubs.Barca, "barcaClub_default");
        }

        [TestMethod]
        public void Serialize_XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();

            var serializer = new XmlSerializer(settings);

            SerializeAndAssert(serializer, Clubs.Barca, "barcaClub_xmlAttributes");
        }

        [TestMethod]
        public void Deserialize_XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            var serializer = new XmlSerializer(settings);

            var expected = Clubs.Barca;
            expected.President = null;

            DeserializeAndAssert(serializer, expected, "barcaClub_xmlAttributes");
        }

        [TestMethod]
        public void Serialize_DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();

            var serializer = new XmlSerializer(settings);

            SerializeAndAssert(serializer, Clubs.Barca, "barcaClub_dataAttributes");
        }

        [TestMethod]
        public void Deserialize_DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            var serializer = new XmlSerializer(settings);

            var expected = Clubs.Barca;
            expected.President = null;

            DeserializeAndAssert(serializer, expected, "barcaClub_dataAttributes");
        }

        private void SerializeAndAssert(XmlSerializer serializer, object obj, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.SerializeAndAssertObject(obj, path);
        }

        private void DeserializeAndAssert(XmlSerializer serializer, object expected, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}