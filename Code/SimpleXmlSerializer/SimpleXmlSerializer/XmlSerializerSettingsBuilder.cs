using System;
using System.Collections.Generic;
using System.Globalization;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettingsBuilder
    {
        private IPropertiesSelector propertiesSelector = new PropertiesSelector();

        private INameProvider nameProvider = new NameProvider();

        private readonly CustomProvider customProvider = new CustomProvider();

        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        private readonly Dictionary<Type, IPrimitiveSerializer> primitiveSerializers = new Dictionary<Type,IPrimitiveSerializer>();

        private bool mapPrimitivesToAttributes;

        public XmlSerializerSettings GetSettings()
        {
            var primitiveProvider = new PrimitiveProvider(formatProvider);
            foreach (var type in primitiveSerializers.Keys)
            {
                primitiveProvider.AddPrimitiveSerializer(type, primitiveSerializers[type]);
            }
            
            if (mapPrimitivesToAttributes)
            {
                nameProvider = new PrimitiveToAttributeNameProvider(nameProvider, primitiveProvider);
            }

            return new XmlSerializerSettings(
                nameProvider,
                primitiveProvider,
                new CollectionNodeProvider(), 
                new ComplexNodeProvider(propertiesSelector),
                customProvider);
        }

        public XmlSerializerSettingsBuilder MapPrimitivesToAttributes()
        {
            mapPrimitivesToAttributes = true;

            return this;
        }

        public XmlSerializerSettingsBuilder UseXmlAttributes()
        {
            nameProvider = new CompositeNameProvider(new[]
                {
                    new XmlAttributesNameProvider(),
                    nameProvider
                });

            propertiesSelector = new XmlAttributesPropertiesSelector();

            return this;
        }

        public XmlSerializerSettingsBuilder UseDataAttributes()
        {
            nameProvider = new CompositeNameProvider(new[]
                {
                    new DataAttributesNameProvider(),
                    nameProvider
                });

            propertiesSelector = new DataAttributesPropertiesSelector();

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
            this.formatProvider = formatProvider;

            return this;
        }
    }
}