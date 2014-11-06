using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class NameProvidersTests
    {
        private const string AssetsDirectory = "Assets\\NameProviders";

        [TestMethod]
        public void DefaultNameProvider()
        {
            var serializer = new XmlSerializer();

            ActAndAssert(serializer, Clubs.Barca, "default");
        }

        [TestMethod]
        public void XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            var serializer = new XmlSerializer(settings);

            var club = Clubs.Barca;
            club.President = null;
            ActAndAssert(serializer, club, "xmlAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            var serializer = new XmlSerializer(settings);

            var club = Clubs.Barca;
            club.President = null;
            ActAndAssert(serializer, club, "dataAttributes");
        }

        private static void ActAndAssert(XmlSerializer serializer, object obj, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");
            serializer.SerializeAndAssert(obj, path);
            serializer.DeserializeAndAssert(obj, path);
        }
    }
}