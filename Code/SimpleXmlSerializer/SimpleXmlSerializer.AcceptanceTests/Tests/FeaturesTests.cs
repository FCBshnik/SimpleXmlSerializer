using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;
using SimpleXmlSerializer.Core;

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

        public class CoachCustomSerializer : ICustomNodeSerializer
        {
            public void Serialize(object value, XmlWriter xmlWriter)
            {
                var coach = (Coach)value;

                xmlWriter.WriteValue(string.Format("{0};{1}", coach.Name, coach.Age));
            }

            public object Deserialize(XmlReader xmlReader)
            {
                var serializedValue = xmlReader.ReadElementString();

                var parts = serializedValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                return new Coach { Name = parts[0], Age = int.Parse(parts[1]) };
            }
        }
    }
}