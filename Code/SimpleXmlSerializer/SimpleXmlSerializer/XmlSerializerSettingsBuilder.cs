using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Responsible to tune up/customize serialization process. 
    /// Produces instance of <see cref="XmlSerializerSettings"/>.
    /// </summary>
    public class XmlSerializerSettingsBuilder
    {
        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        private IPropertiesSelector propertiesSelector = new PublicPropertiesSelector();

        private readonly CustomNodeProvider customProvider = new CustomNodeProvider();

        private readonly PrimitiveNodeProvider primitiveProvider;

        private readonly List<ICollectionNodeProvider> collectionProviders = new List<ICollectionNodeProvider>();

        private INameProvider nameProvider;

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
            var collectionProvider = new CompositeCollectionNodeProvider(collectionProviders);

            var defaultNameProvider = new NameProvider(new CamelCaseNamingConvention(), collectionProvider);
            if (nameProvider != null)
            {
                nameProvider = new CompositeNameProvider(new[] { nameProvider, defaultNameProvider });
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

        /// <summary>
        /// Specifies to serialize primitive types to attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder SerializePrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;
            return this;
        }

        /// <summary>
        /// Specifies to use xml* attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            nameProvider = new XmlAttributesNameProvider();
            propertiesSelector = new KeyValuePairPropertiesSelector(new XmlAttributesPropertiesSelector());
            return this;
        }

        /// <summary>
        /// Specifies to use data contract attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            nameProvider = new DataAttributesNameProvider();
            collectionProviders.Prepend(new DataAttributeCollectionProvider());
            propertiesSelector = new KeyValuePairPropertiesSelector(new DataAttributesPropertiesSelector());
            return this;
        }

        public XmlSerializerSettingsBuilder AddCustomSerializer(Type type, ICustomSerializer customSerializer)
        {
            customProvider.AddSerializer(type, customSerializer);
            return this;
        }

        public XmlSerializerSettingsBuilder AddPrimitiveSerializer(Type type, IPrimitiveSerializer primitiveSerializer)
        {
            primitiveProvider.SetPrimitiveSerializer(type, primitiveSerializer);
            return this;
        }

        public XmlSerializerSettingsBuilder UseFormatProvider(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
            return this;
        }
    }
}