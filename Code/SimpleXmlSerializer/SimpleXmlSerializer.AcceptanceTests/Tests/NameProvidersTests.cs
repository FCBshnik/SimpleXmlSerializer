using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class NameProvidersTests : TestsBase
    {
        [TestMethod]
        public void DefaultNameProvider()
        {
            serializer = new XmlSerializer();

            ActAndAssert(ComplexWithComplexes.Numbers, "default");
        }

        [TestMethod]
        public void XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "xmlAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "dataAttributes");
        }
    }
}