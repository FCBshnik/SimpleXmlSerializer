using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class FeaturesTests : TestsBase
    {
        [TestMethod]
        public void PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().MapPrimitivesToAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(Clubs.Barca, "primitivesToAttributes");
        }

        [TestMethod]
        public void CustomSerializer()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .AddCustomSerializer(typeof(Coach), new CoachCustomSerializer())
                .GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(Clubs.Barca, "customSerializer");
        }
        
        public class CoachCustomSerializer : ICustomSerializer
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