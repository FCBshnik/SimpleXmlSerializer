using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Core.Serializers;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Responsible to customize serialization process. Produces instance of <see cref="XmlSerializerSettings"/>.
    /// </summary>
    public class XmlSerializerSettingsBuilder
    {
        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;
        private XmlElementName defaultCollectionName = new XmlElementName("Collection");
        private XmlElementName defaultCollectionItemName = new XmlElementName("Add");
        private INamingConvention namingConvention = new NoNamingConvention();

        private IPrimitiveTypeProvider customPrimitiveTypeProvider;
        private readonly List<IPrimitiveTypeProvider> extraPrimitiveTypeProviders = new List<IPrimitiveTypeProvider>();
        private readonly IDictionary<Type, IPrimitiveSerializer> extraPrimitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>();

        private ICollectionTypeProvider customCollectionTypeProvider;
        private readonly List<ICollectionTypeProvider> extraCollectionTypeProviders = new List<ICollectionTypeProvider>();

        private IPropertiesProvider customPropertiesProvider;
        private readonly List<IPropertiesProvider> extraPropertiesProviders = new List<IPropertiesProvider>();

        private ICompositeTypeProvider customCompositeTypeProvider;
        private readonly List<ICompositeTypeProvider> extraCompositeTypeProviders = new List<ICompositeTypeProvider>();

        private INameProvider customNameProvider;
        private readonly List<INameProvider> extraNameProviders = new List<INameProvider>();

        /// <summary>
        /// Returns <see cref="XmlSerializerSettings"/> based on current configured state.
        /// </summary>
        public XmlSerializerSettings GetSettings()
        {
            var primitiveProvider = customPrimitiveTypeProvider ?? BuildPrimitiveTypeProvider();
            var collectionProvider = customCollectionTypeProvider ?? BuildCollectionTypeProvider();
            var propertiesProvider = customPropertiesProvider ?? BuildPropertiesProvider();
            var compositeTypeProvider = customCompositeTypeProvider ?? BuildCompositeTypeProvider(propertiesProvider);
            var nameProvider = customNameProvider ?? BuildNameProvider(collectionProvider);

            return new XmlSerializerSettings(
                new CachingNameProvider(nameProvider),
                primitiveProvider,
                collectionProvider,
                compositeTypeProvider);
        }

        /// <summary>
        /// Sets default name for collection elements of xml document.
        /// </summary>
        public XmlSerializerSettingsBuilder SetDefaultCollectionName(string name)
        {
            defaultCollectionName = new XmlElementName(name);
            return this;
        }

        /// <summary>
        /// Sets default name for item elements of collection element of xml document.
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
        public XmlSerializerSettingsBuilder SetCamelCaseNamingConvention()
        {
            return SetNamingConvention(new CamelCaseNamingConvention());
        }

        /// <summary>
        /// Specifies to use specified <see cref="ICollectionTypeProvider"/>. It overwrites
        /// default providers and providers specified using <see cref="AddCollectionTypeProvider"/> method.
        /// </summary>
        public XmlSerializerSettingsBuilder SetCollectionTypeProvider(ICollectionTypeProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            customCollectionTypeProvider = provider;
            return this;
        }

        /// <summary>
        /// Adds specified <see cref="ICollectionTypeProvider"/> to start of <see cref="ICollectionTypeProvider"/> chain. 
        /// It has precedence to default providers.
        /// </summary>
        public XmlSerializerSettingsBuilder AddCollectionTypeProvider(ICollectionTypeProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            extraCollectionTypeProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="IPropertiesProvider"/>. It overwrites
        /// default providers and providers specified using <see cref="AddPropertiesProvider"/> method.
        /// </summary>
        public XmlSerializerSettingsBuilder SetPropertiesProvider(IPropertiesProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            customPropertiesProvider = provider;
            return this;
        }

        /// <summary>
        /// Adds specified <see cref="IPropertiesProvider"/> to start of <see cref="IPropertiesProvider"/> chain. 
        /// It has precedence to default providers.
        /// </summary>
        public XmlSerializerSettingsBuilder AddPropertiesProvider(IPropertiesProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            extraPropertiesProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="ICompositeTypeProvider"/>. It overwrites
        /// default providers and providers specified using <see cref="AddCompositeTypeProvider"/> method.
        /// </summary>
        public XmlSerializerSettingsBuilder SetCompositeTypeProvider(ICompositeTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            customCompositeTypeProvider = provider;
            return this;
        }

        /// <summary>
        /// Adds specified <see cref="ICompositeTypeProvider"/> to start of <see cref="ICompositeTypeProvider"/> chain. 
        /// It has precedence to default providers.
        /// </summary>
        public XmlSerializerSettingsBuilder AddCompositeTypeProvider(ICompositeTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            extraCompositeTypeProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="INameProvider"/>. It overwrites
        /// default providers and providers specified using <see cref="AddNameProvider"/> method.
        /// </summary>
        public XmlSerializerSettingsBuilder SetNameProvider(INameProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            customNameProvider = provider;
            return this;
        }

        /// <summary>
        /// Adds specified <see cref="INameProvider"/> to start of <see cref="INameProvider"/> chain. 
        /// It has precedence to default providers.
        /// </summary>
        public XmlSerializerSettingsBuilder AddNameProvider(INameProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            extraNameProviders.Add(provider);
            return this;
        }

        /// <summary>
        /// Specifies to use specified <see cref="IPrimitiveTypeProvider"/>. It overwrites
        /// default providers and providers specified using <see cref="AddPrimitiveTypeProvider"/> method.
        /// </summary>
        public XmlSerializerSettingsBuilder SetPrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            customPrimitiveTypeProvider = provider;
            return this;
        }

        /// <summary>
        /// Adds specified <see cref="IPrimitiveTypeProvider"/> to start of <see cref="IPrimitiveTypeProvider"/> chain. 
        /// It has precedence to default providers.
        /// </summary>
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

        /// <summary>
        /// Specifies to use xml attributes from <see cref="System.Xml.Serialization"/> namespace.
        /// </summary>
        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            AddNameProvider(new XmlAttributesNameProvider());
            AddPropertiesProvider(new XmlAttributesPropertiesProvider());
            return this;
        }

        /// <summary>
        /// Specifies to use data contract attributes from <see cref="System.Runtime.Serialization"/> namespace.
        /// </summary>
        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            AddNameProvider(new DataAttributesNameProvider());
            AddPropertiesProvider(new DataAttributesPropertiesProvider());
            AddCollectionTypeProvider(new DataAttributesCollectionProvider());
            return this;
        }

        private IPrimitiveTypeProvider BuildPrimitiveTypeProvider()
        {
            // merge default primitive serializers with additionally added (which have higher precedence)
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

            return new CDataPrimitiveTypeProvider(primitiveProvider);
        }

        private ICollectionTypeProvider BuildCollectionTypeProvider()
        {
            var collectionProviders = extraCollectionTypeProviders.Concat(GetDefaultCollectionTypeProviders()).ToList();
            return new ChainedCollectionTypeProvider(collectionProviders);
        }

        private IPropertiesProvider BuildPropertiesProvider()
        {
            var defaultPublicPropertiesProvider = new PublicPropertiesProvider();
            if (extraPropertiesProviders.Any())
            {
                return new ChainedPropertiesProvider(extraPropertiesProviders.Concat(new[] { defaultPublicPropertiesProvider }));
            }

            return defaultPublicPropertiesProvider;
        }

        private ICompositeTypeProvider BuildCompositeTypeProvider(IPropertiesProvider propertiesProvider)
        {
            var providers = extraCompositeTypeProviders.Concat(GetDefaultCompositeTypeProviders(propertiesProvider)).ToList();
            return new ChainedCompositeTypeProvider(providers);
        }

        private INameProvider BuildNameProvider(ICollectionTypeProvider collectionProvider)
        {
            var defaultNameProvider = new NameProvider(namingConvention, collectionProvider, defaultCollectionName, defaultCollectionItemName);
            var nameProviders = extraNameProviders.Concat(new []{ defaultNameProvider }).ToList();
            return new CompositeNameProvider(nameProviders);
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

        private static IEnumerable<ICollectionTypeProvider> GetDefaultCollectionTypeProviders()
        {
            yield return new DictionaryCollectionTypeProvider();
            yield return new ArrayCollectionTypeProvider();
            yield return new CollectionTypeProvider();
        }

        private static IEnumerable<ICompositeTypeProvider> GetDefaultCompositeTypeProviders(IPropertiesProvider propertiesProvider)
        {
            yield return new KeyValuePairCompositeTypeProvider();
            yield return new CompositeTypeProvider(propertiesProvider);
        }
    }
}