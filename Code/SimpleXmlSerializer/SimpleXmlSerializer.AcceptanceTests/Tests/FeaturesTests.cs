using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Tests.Features;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class FeaturesTests
    {
        private const string AssetsDirectory = "Assets\\Features";

        [TestMethod]
        public void Serialize_PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().MapPrimitivesToAttributes().GetSettings();

            var serializer = new XmlSerializer(settings);

            SerializeAndAssert(serializer, Clubs.Barca, "primitivesToAttributes");
        }

        [TestMethod]
        public void Deserialize_PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().MapPrimitivesToAttributes().GetSettings();

            var serializer = new XmlSerializer(settings);

            DeserializeAndAssert(serializer, Clubs.Barca, "primitivesToAttributes");
        }

        [TestMethod]
        public void Serialize_CustomSerializer()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .AddCustomSerializer(typeof(Coach), new CoachCustomSerializer())
                .GetSettings();

            var serializer = new XmlSerializer(settings);

            SerializeAndAssert(serializer, Clubs.Barca, "customSerializer");
        }

        [TestMethod]
        public void Deserialize_CustomSerializer()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .AddCustomSerializer(typeof(Coach), new CoachCustomSerializer())
                .GetSettings();

            var serializer = new XmlSerializer(settings);

            DeserializeAndAssert(serializer, Clubs.Barca, "customSerializer");
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