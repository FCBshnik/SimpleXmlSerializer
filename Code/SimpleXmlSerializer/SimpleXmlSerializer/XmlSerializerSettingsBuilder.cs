using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Core.Serializers;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Responsible to customize serialization process. Produces instance of <see cref="XmlSerializerSettings"/>.
    /// </summary>
    public class XmlSerializerSettingsBuilder
    {
        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;
        private bool mapPrimitivesToAttributes;
        private XmlElementName defaultCollectionName = new XmlElementName("Collection");
        private XmlElementName defaultCollectionItemName = new XmlElementName("Add");
        private INamingConvention namingConvention = new NoNamingConvention();

        private IPrimitiveTypeProvider customPrimitiveTypeProvider;
        private readonly List<IPrimitiveTypeProvider> extraPrimitiveTypeProviders = new List<IPrimitiveTypeProvider>();
        private readonly IDictionary<Type, IPrimitiveSerializer> extraPrimitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>();

        private ICollectionTypeProvider customCollectionTypeProvider;
        private readonly List<ICollectionTypeProvider> extraCollectionTypeProviders = new List<ICollectionTypeProvider>();

        private IPropertiesSelector customPropertiesSelector;
        private readonly List<IPropertiesSelector> extraPropertiesSelectors = new List<IPropertiesSelector>();

        private INameProvider customNameProvider;
        private readonly List<INameProvider> extraNameProviders = new List<INameProvider>();

        public XmlSerializerSettings GetSettings()
        {
            var primitiveProvider = BuildPrimitiveTypeProvider();
            var collectionProvider = BuildCollectionTypeProvider();
            var propertiesSelector = BuildPropertiesSelector();
            var compositeTypeProvider = BuildCompositeTypeProvider(propertiesSelector);
            var nameProvider = BuildNameProvider(primitiveProvider, collectionProvider);

            return new XmlSerializerSettings(
                new CachingNameProvider(nameProvider),
                primitiveProvider,
                collectionProvider,
                compositeTypeProvider);
        }

        /// <summary>
        /// Sets...
        /// </summary>
        public XmlSerializerSettingsBuilder SetDefaultCollectionName(string name)
        {
            defaultCollectionName = new XmlElementName(name);
            return this;
        }

        /// <summary>
        /// Sets...
        /// </summary>
        public XmlSerializerSettingsBuilder SetDefaultCollectionItemName(string name)
        {
            defaultCollectionItemName = new XmlElementName(name);
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="INamingConvention"/>.
        /// Naming convention is used only when no markup attributes are used.
        /// </summary>
        public XmlSerializerSettingsBuilder SetNamingConvention(INamingConvention namingConvention)
        {
            if (namingConvention == null)
                throw new ArgumentNullException("namingConvention");

            this.namingConvention = namingConvention;
            return this;
        }

        /// <summary>
        /// Specifies to use camel case naming convention.
        /// Naming convention is used only when no markup attributes are used.
        /// </summary>
        public XmlSerializerSettingsBuilder UseCamelCaseNamingConvention()
        {
            return SetNamingConvention(new CamelCaseNamingConvention());
        }

        /// <summary>
        /// Specifies to serialize primitives to xml attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder SerializePrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;
            return this;
        }

        public XmlSerializerSettingsBuilder SetCollectionTypeProvider(ICollectionTypeProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            customCollectionTypeProvider = provider;
            return this;
        }

        public XmlSerializerSettingsBuilder AddCollectionTypeProvider(ICollectionTypeProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            extraCollectionTypeProviders.Add(provider);
            return this;
        }

        public XmlSerializerSettingsBuilder SetPropertiesSelector(IPropertiesSelector selector)
        {
            if (selector == null) 
                throw new ArgumentNullException("selector");

            customPropertiesSelector = selector;
            return this;
        }

        public XmlSerializerSettingsBuilder AddPropertiesSelector(IPropertiesSelector selector)
        {
            if (selector == null) 
                throw new ArgumentNullException("selector");

            extraPropertiesSelectors.Add(selector);
            return this;
        }

        public XmlSerializerSettingsBuilder SetNameProvider(INameProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            customNameProvider = provider;
            return this;
        }

        public XmlSerializerSettingsBuilder AddNameProvider(INameProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            extraNameProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies to use xml attributes from <see cref="System.Xml.Serialization"/> namespace.
        /// </summary>
        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            AddNameProvider(new XmlAttributesNameProvider());
            AddPropertiesSelector(new XmlAttributesPropertiesSelector());
            return this;
        }

        /// <summary>
        /// Specifies to use data contract attributes from <see cref="System.Runtime.Serialization"/> namespace.
        /// </summary>
        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            AddNameProvider(new DataAttributesNameProvider());
            AddPropertiesSelector(new DataAttributesPropertiesSelector());
            AddCollectionTypeProvider(new DataAttributeCollectionProvider());
            return this;
        }

        public XmlSerializerSettingsBuilder SetPrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            customPrimitiveTypeProvider = provider;
            return this;
        }

        public XmlSerializerSettingsBuilder AddPrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            extraPrimitiveTypeProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies that type should be serialized as primitive.
        /// </summary>
        public XmlSerializerSettingsBuilder SetPrimitiveSerializer(Type type, IPrimitiveSerializer serializer)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            extraPrimitiveSerializers[type] = serializer;
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="IFormatProvider"/>.
        /// </summary>
        public XmlSerializerSettingsBuilder SetFormatProvider(IFormatProvider formatProvider)
        {
            if (formatProvider == null)
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
            return this;
        }

        private IPrimitiveTypeProvider BuildPrimitiveTypeProvider()
        {
            // use custom if specified
            if (customPrimitiveTypeProvider != null)
            {
                return customPrimitiveTypeProvider;
            }

            // merge custom primitive serializers with additional (which have higher precedence)
            var primitiveSerializers = GetDefaultPrimitiveSerializers();
            primitiveSerializers = extraPrimitiveSerializers.Merge(primitiveSerializers);

            var primitiveProviders = extraPrimitiveTypeProviders.Concat(new IPrimitiveTypeProvider[]
                {
                    new PrimitiveTypeProvider(primitiveSerializers), new EnumPrimitiveTypeProvider()
                });

            IPrimitiveTypeProvider primitiveProvider = new ChainedPrimitiveTypeProvider(primitiveProviders.ToList());

            primitiveProvider = new ChainedPrimitiveTypeProvider(new[]
                {
                    primitiveProvider, new NullablePrimitiveTypeProvider(primitiveProvider)
                });

            return primitiveProvider;
        }

        private ICollectionTypeProvider BuildCollectionTypeProvider()
        {
            if (customCollectionTypeProvider != null)
            {
                return customCollectionTypeProvider;
            }

            var collectionProviders = extraCollectionTypeProviders.Concat(GetDefaultCollectionTypeProviders()).ToList();
            return new ChainedCollectionTypeProvider(collectionProviders);
        }

        private IPropertiesSelector BuildPropertiesSelector()
        {
            if (customPropertiesSelector != null)
            {
                return customPropertiesSelector;
            }

            if (extraPropertiesSelectors.Any())
            {
                return new ChainedPropertiesSelector(extraPropertiesSelectors);
            }

            return new PublicPropertiesSelector();
        }

        private ICompositeTypeProvider BuildCompositeTypeProvider(IPropertiesSelector propertiesSelector)
        {
            return new ChainedCompositeTypeProvider(new ICompositeTypeProvider []
                {
                    new KeyValuePairCompositeTypeProvider(new KeyValuePairPropertiesSelector()),
                    new CompositeTypeProvider(propertiesSelector)
                });
        }

        private INameProvider BuildNameProvider(IPrimitiveTypeProvider primitiveProvider, ICollectionTypeProvider collectionProvider)
        {
            if (customNameProvider != null)
            {
                return customNameProvider;
            }

            var nameProviders = extraNameProviders.ToList();
            nameProviders.Add(new NameProvider(namingConvention, collectionProvider, defaultCollectionName, defaultCollectionItemName));

            INameProvider nameProvider = new CompositeNameProvider(nameProviders);

            if (mapPrimitivesToAttributes)
            {
                nameProvider = new PrimitiveToAttributeNameProvider(nameProvider, primitiveProvider);
            }

            return nameProvider;
        }

        private IDictionary<Type, IPrimitiveSerializer> GetDefaultPrimitiveSerializers()
        {
            return new Dictionary<Type, IPrimitiveSerializer>
                {
                    { typeof(char), new CharSerializer() },
                    { typeof(string), new StringSerializer() },
                    { typeof(short), new ShortSerializer(formatProvider) },
                    { typeof(ushort), new UshortSerializer(formatProvider) },
                    { typeof(byte), new ByteSerializer(formatProvider) },
                    { typeof(sbyte), new SbyteSerializer(formatProvider) },
                    { typeof(int), new IntSerializer(formatProvider) },
                    { typeof(uint), new UintSerializer(formatProvider) },
                    { typeof(long), new LongSerializer(formatProvider) },
                    { typeof(ulong), new UlongSerializer(formatProvider) },
                    { typeof(float), new FloatSerializer(formatProvider) },
                    { typeof(double), new DoubleSerializer(formatProvider) },
                    { typeof(decimal), new DecimalSerializer(formatProvider) },
                    { typeof(bool), new BoolSerializer(formatProvider) },
                    { typeof(TimeSpan), new TimeSpanSerializer(string.Empty, formatProvider) },
                    { typeof(DateTime), new DateTimeSerializer(formatProvider) },
                    { typeof(DateTimeOffset), new DateTimeOffsetSerializer(formatProvider) },
                    { typeof(Uri), new UriSerializer() },
                    { typeof(Guid), new GuidSerializer(string.Empty, formatProvider) },
                    { typeof(Type), new TypeSerializer() }
                };
        }

        private IEnumerable<ICollectionTypeProvider> GetDefaultCollectionTypeProviders()
        {
            yield return new DictionaryCollectionTypeProvider();
            yield return new ArrayCollectionTypeProvider();
            yield return new CollectionTypeProvider();
        }
    }
}