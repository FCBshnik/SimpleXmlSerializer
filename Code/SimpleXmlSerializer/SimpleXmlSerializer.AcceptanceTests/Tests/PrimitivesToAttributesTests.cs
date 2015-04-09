using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.IntegrationTests.Dto;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class PrimitivesToAttributesTests : TestsBase
    {
        [TestMethod]
        public void PrimitivesToAttributes()
        {
            var settings = GetSettingsBuilder().SerializePrimitivesToAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "primitivesToAttributes");
        }

        [TestMethod]
        public void PrimitivesToAttributesWithNull()
        {
            var settings = GetSettingsBuilder().SerializePrimitivesToAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            var numbers = CompositeWithComposites.Numbers;
            numbers.Two.String = null;
            ActAndAssert(numbers, "primitivesToAttributesWithNull");
        }
    }
}