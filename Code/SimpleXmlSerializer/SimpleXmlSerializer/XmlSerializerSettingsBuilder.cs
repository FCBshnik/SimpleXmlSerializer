using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettingsBuilder
    {
        // todo: use composite properties selector
        private IPropertiesSelector propertiesSelector = new PropertiesSelector();

        private readonly CustomNodeProvider customProvider = new CustomNodeProvider();

        private IFormatProvider internalFormatProvider = CultureInfo.InvariantCulture;

        private readonly PrimitiveNodeProvider primitiveProvider;

        private readonly List<ICollectionNodeProvider> collectionProviders = new List<ICollectionNodeProvider>();

        private readonly List<INameProvider> nameProviders = new List<INameProvider>();

        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>();

        private bool mapPrimitivesToAttributes;

        public XmlSerializerSettingsBuilder()
        {
            primitiveProvider = new PrimitiveNodeProvider(internalFormatProvider);

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

            INameProvider nameProvider = new NameProvider(new CamelCaseNamingConvention(), collectionProvider);
            if (mapPrimitivesToAttributes)
            {
                // todo: cache primitive provider
                nameProvider = new PrimitiveToAttributeNameProvider(nameProvider, primitiveProvider);
            }

            nameProviders.Add(nameProvider);
            nameProvider = new CompositeNameProvider(nameProviders);

            return new XmlSerializerSettings(
                new CachingNameProvider(nameProvider),
                primitiveProvider, // todo: cache primitive provider
                collectionProvider, 
                new ComplexNodeProvider(propertiesSelector),
                customProvider);
        }

        // todo: rename to "SerializePri..."
        public XmlSerializerSettingsBuilder MapPrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;
            return this;
        }

        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            nameProviders.Prepend(new XmlAttributesNameProvider());
            propertiesSelector = new XmlAttributesPropertiesSelector();
            return this;
        }

        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            nameProviders.Prepend(new DataAttributesNameProvider());
            collectionProviders.Prepend(new DataAttributeCollectionProvider());
            propertiesSelector = new XmlAttributesPropertiesSelector();
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

        public XmlSerializerSettingsBuilder WithFormatProvider(IFormatProvider formatProvider)
        {
            internalFormatProvider = formatProvider;
            return this;
        }
    }
}