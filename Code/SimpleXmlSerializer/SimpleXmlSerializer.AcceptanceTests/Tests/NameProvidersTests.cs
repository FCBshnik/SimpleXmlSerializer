using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class NameProvidersTests : TestsBase
    {
        [TestMethod]
        public void DefaultNameProvider()
        {
            serializer = new XmlSerializer();

            ActAndAssert(Clubs.Barca, "default");
        }

        [TestMethod]
        public void XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            var club = Clubs.Barca;
            club.President = null;
            ActAndAssert(club, "xmlAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            var club = Clubs.Barca;
            club.President = null;
            ActAndAssert(club, "dataAttributes");
        }
    }
}