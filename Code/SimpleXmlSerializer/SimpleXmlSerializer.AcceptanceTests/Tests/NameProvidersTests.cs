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
        public void XmlAttributesNameProviderWithPrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .UseXmlAttributes()
                .SerializePrimitivesToAttributes()
                .GetSettings();

            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "xmlAttributesWithPrimitivesToAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "dataAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProviderWithPrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .UseDataAttributes()
                .SerializePrimitivesToAttributes()
                .GetSettings();

            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithComplexes.Numbers, "dataAttributesWithPrimitivesToAttributes");
        }

        [TestMethod]
        public void CollectionDataAttributeForCollection()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(CustomCollection.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericCollection()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericCollection<string>.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForDictionary()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(CustomDictionary.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericDictionary()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericDictionary<string, string>.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForProperties()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            serializer = new XmlSerializer(settings);

            ActAndAssert(ComplexWithCustomCollections.Numbers, "collectionDataAttributeForProperties");
        }
    }
}