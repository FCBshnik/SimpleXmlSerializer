using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class NameProvidersTests : TestsBase
    {
        [TestMethod]
        public void DefaultNameProvider()
        {
            Serializer = new XmlSerializer();

            ActAndAssert(CompositeWithComposites.Numbers, "default");
        }

        [TestMethod]
        public void XmlAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "xmlAttributes");
        }

        [TestMethod]
        public void XmlAttributesNameProvider_Order()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithPrimitivesOrder.One, "xmlAttributes_order");
        }

        [TestMethod]
        public void XmlAttributesNameProvider_PrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .UseXmlAttributes()
                .SerializePrimitivesToAttributes()
                .GetSettings();

            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "xmlAttributes_primitivesToAttributes");
        }

        [TestMethod]
        public void XmlAttributesNameProvider_XmlArrayAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithCollectionsXmlArrayAttrs.Numbers, "xmlAttributes_xmlArrayAttributes");
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void XmlAttributesNameProvider_Serialize_ConflictElementsNames()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void XmlAttributesNameProvider_Serialize_ConflictAttributesNames()
        {
            var settings = new XmlSerializerSettingsBuilder().UseXmlAttributes().SerializePrimitivesToAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [TestMethod]
        public void DataAttributesNameProvider()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "dataAttributes");
        }

        [TestMethod]
        public void DataAttributesNameProvider_Order()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithPrimitivesOrder.One, "dataAttributes_order");
        }

        [TestMethod]
        public void DataAttributesNameProviderWithPrimitivesToAttributes()
        {
            var settings = new XmlSerializerSettingsBuilder()
                .UseDataAttributes()
                .SerializePrimitivesToAttributes()
                .GetSettings();

            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithComposites.Numbers, "dataAttributesWithPrimitivesToAttributes");
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void DataAttributesNameProvider_Serialize_ConflictElementsNames()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [ExpectedException(typeof(SerializationException))]
        [TestMethod]
        public void DataAttributesNameProvider_Serialize_ConflictAttributesNames()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().SerializePrimitivesToAttributes().GetSettings();

            Serializer = new XmlSerializer(settings);

            Serializer.SerializeToString(CompositeWithPrimitivesConflictElementNames.One);
        }

        [TestMethod]
        public void CollectionDataAttributeForCollection()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomCollection.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericCollection()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericCollection<string>.Numbers, "collectionDataAttributeForCollectionType");
        }

        [TestMethod]
        public void CollectionDataAttributeForDictionary()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomDictionary.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForGenericDictionary()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CustomGenericDictionary<string, string>.Numbers, "collectionDataAttributeForDictionaryType");
        }

        [TestMethod]
        public void CollectionDataAttributeForProperties()
        {
            var settings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            Serializer = new XmlSerializer(settings);

            ActAndAssert(CompositeWithCustomCollections.Numbers, "collectionDataAttributeForProperties");
        }
    }
}