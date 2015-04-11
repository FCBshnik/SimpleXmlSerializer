using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.IntegrationTests.Dto;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class NameProvidersTests : TestsBase
    {
        [TestMethod]
        public void DefaultNameProvider()
        {
            Serializer = new XmlSerializer(GetSettingsBuilder().GetSettings());

            ActAndAssert(CompositeWithComposites.Numbers, "default");
        }

        [TestMethod]
        public void XmlAttributesNameProvider()
        {
            var settings = GetSettingsBuilder().UseXmlAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "xmlAttributes");
        }

        [TestMethod]
        public void XmlAttributesNameProvider_Order()
        {
            var settings = GetSettingsBuilder().UseXmlAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithPrimitivesOrder.One, "xmlAttributes_order");
        }

        [TestMethod]
        public void XmlAttributesNameProvider_XmlArrayAttributes()
        {
            var settings = GetSettingsBuilder().UseXmlAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithCollectionsXmlArrayAttrs.Numbers, "xmlAttributes_xmlArrayAttributes");
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void XmlAttributesNameProvider_Serialize_ConflictElementsNames()
        {
            var settings = GetSettingsBuilder().UseXmlAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "dataAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider_Order()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithPrimitivesOrder.One, "dataAttributes_order");
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void DataAttributesNameProvider_Serialize_ConflictElementsNames()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [TestMethod]
        public void CollectionDataAttributeForCollection()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomCollection.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericCollection()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericCollection<string>.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForDictionary()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomDictionary.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericDictionary()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericDictionary<string, string>.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForProperties()
        {
            var settings = GetSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithCustomCollections.Numbers, "collectionDataAttributeForProperties");
        }
    }
}