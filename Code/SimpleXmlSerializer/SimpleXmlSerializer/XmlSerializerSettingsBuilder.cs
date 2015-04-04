using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Core.Serializers;
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

        private IPrimitiveTypeProvider primitiveProvider;

        private readonly List<ICollectionTypeProvider> collectionProviders = new List<ICollectionTypeProvider>();

        private INameProvider nameProvider;

        private bool mapPrimitivesToAttributes;

        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers;

        public XmlSerializerSettingsBuilder()
        {
            primitiveSerializers = new Dictionary<Type, IPrimitiveSerializer>
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
                    { typeof(TimeSpan), new TimeSpanSerializer() },
                    { typeof(DateTime), new DateTimeSerializer(formatProvider) },
                    { typeof(DateTimeOffset), new DateTimeOffsetSerializer(formatProvider) },
                    { typeof(Uri), new UriSerializer() },
                    { typeof(Guid), new GuidSerializer(string.Empty, formatProvider) },
                    { typeof(Type), new TypeSerializer() }
                };

            collectionProviders.Add(new DictionaryCollectionTypeProvider());
            collectionProviders.Add(new ArrayCollectionTypeProvider());
            collectionProviders.Add(new CollectionTypeProvider());
        }

        public XmlSerializerSettings GetSettings()
        {
            // primitives
            primitiveProvider = new ChainedPrimitiveTypeProvider(new IPrimitiveTypeProvider[]
                {
                    new PrimitiveTypeProvider(primitiveSerializers), new EnumPrimitiveTypeProvider()
                });
            primitiveProvider = new ChainedPrimitiveTypeProvider(new []
                {
                    primitiveProvider, new NullablePrimitiveTypeProvider(primitiveProvider)
                });

            // collections
            var collectionProvider = new ChainedCollectionTypeProvider(collectionProviders);

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

            propertiesSelector = new ChainedPropertiesSelector(new[] { new KeyValuePairPropertiesSelector(), propertiesSelector });

            return new XmlSerializerSettings(
                new CachingNameProvider(nameProvider),
                primitiveProvider,
                collectionProvider,
                new CompositeTypeProvider(propertiesSelector));
        }

        /// <summary>
        /// Specifies to serialize primitives to attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder SerializePrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;
            return this;
        }

        /// <summary>
        /// Specifies to use Xml* attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            nameProvider = new XmlAttributesNameProvider();
            propertiesSelector = new XmlAttributesPropertiesSelector();
            return this;
        }

        /// <summary>
        /// Specifies to use data contract attributes.
        /// </summary>
        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            nameProvider = new DataAttributesNameProvider();
            collectionProviders.Prepend(new DataAttributeCollectionProvider());
            propertiesSelector = new DataAttributesPropertiesSelector();
            return this;
        }

        public XmlSerializerSettingsBuilder SetPrimitiveSerializer(Type type, IPrimitiveSerializer serializer)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            primitiveSerializers[type] = serializer;
            return this;
        }

        public XmlSerializerSettingsBuilder SetFormatProvider(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
            return this;
        }
    }
}