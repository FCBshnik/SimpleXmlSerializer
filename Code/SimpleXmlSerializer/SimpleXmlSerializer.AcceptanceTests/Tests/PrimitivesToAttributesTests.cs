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
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "primitivesToAttributes");
        }

        [TestMethod]
        public void PrimitivesToAttributesWithNull()
        {
            var settings = new XmlSerializerSettingsBuilder().SerializePrimitivesToAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            var numbers = ComplexWithComplexes.Numbers;
            numbers.Two.String = null;
            ActAndAssert(numbers, "primitivesToAttributesWithNull");
        }
    }
}