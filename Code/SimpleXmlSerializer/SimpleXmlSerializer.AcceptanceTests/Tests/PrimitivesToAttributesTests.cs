using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class PrimitivesToAttributesTests : TestsBase
    {
        [TestMethod]
        public void PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().SerializePrimitivesToAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "primitivesToAttributes");
        }

        [TestMethod]
        public void PrimitivesToAttributesWithNull()
        {
            var settings = new XmlSerializerSettingsBuilder().SerializePrimitivesToAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            var numbers = CompositeWithComposites.Numbers;
            numbers.Two.String = null;
            ActAndAssert(numbers, "primitivesToAttributesWithNull");
        }
    }
}