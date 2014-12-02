using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettingsBuilder
    {
        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        private IPropertiesSelector propertiesSelector = new PropertiesSelector();

        private readonly CustomNodeProvider customProvider = new CustomNodeProvider();

        private readonly PrimitiveNodeProvider primitiveProvider;

        private readonly List<ICollectionNodeProvider> collectionProviders = new List<ICollectionNodeProvider>();

        private INameProvider nameProvider;

        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>();

        private bool mapPrimitivesToAttributes;

        public XmlSerializerSettingsBuilder()
        {
            primitiveProvider = new PrimitiveNodeProvider(formatProvider);

            collectionProviders.Add(new DictionaryCollectionNodeProvider());
            collectionProviders.Add(new ArrayCollectionNodeProvider());
            collectionProviders.Add(new CollectionNodeProvider());
        }

        public XmlSerializerSettings GetSettings()
        {
            foreach (var type in primitiveSerializers.Keys)
            {
                primitiveProvider.SetPrimitiveSerializer(type, primitiveSerializers[type]);
            }

            var collectionProvider = new CompositeCollectionNodeProvider(collectionProviders);

            var defaultNameProvider = new NameProvider(new CamelCaseNamingConvention(), collectionProvider);
            if (nameProvider != null)
            {
                nameProvider = new CompositeNameProvider(new[] {nameProvider, defaultNameProvider});
            }
            else
            {
                nameProvider = defaultNameProvider;
            }

            if (mapPrimitivesToAttributes)
            {
                nameProvider = new PrimitiveToAttributeNameProvider(nameProvider, primitiveProvider);
            }

            return new XmlSerializerSettings(
                new CachingNameProvider(nameProvider),
                primitiveProvider, 
                collectionProvider, 
                new ComplexNodeProvider(propertiesSelector),
                customProvider);
        }

        public XmlSerializerSettingsBuilder SerializePrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;
            return this;
        }

        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            nameProvider = new XmlAttributesNameProvider();
            propertiesSelector = new SpecialPropertiesSelector(new PropertiesSelector(), new XmlAttributesPropertiesSelector());
            return this;
        }

        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            nameProvider = new DataAttributesNameProvider();
            collectionProviders.Prepend(new DataAttributeCollectionProvider());
            propertiesSelector = new SpecialPropertiesSelector(new PropertiesSelector(), new DataAttributesPropertiesSelector());
            return this;
        }

        public XmlSerializerSettingsBuilder AddCustomSerializer(Type type, ICustomSerializer customSerializer)
        {
            customProvider.AddSerializer(type, customSerializer);
            return this;
        }

        public XmlSerializerSettingsBuilder AddPrimitiveSerializer(Type type, IPrimitiveSerializer primitiveSerializer)
        {
            primitiveSerializers[type] = primitiveSerializer;
            return this;
        }

        public XmlSerializerSettingsBuilder UseFormatProvider(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
            return this;
        }
    }
}