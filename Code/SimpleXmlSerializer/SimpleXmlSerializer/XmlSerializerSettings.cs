using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettings
    {
        private static readonly XmlSerializerSettings @default;

        private readonly INameProvider nameProvider;

        private readonly IPrimitiveProvider primitiveProvider;

        private readonly ICollectionProvider collectionProvider;

        private readonly IComplexProvider complexProvider;

        private readonly ICustomProvider customProvider;

        static XmlSerializerSettings()
        {
            @default = new XmlSerializerSettingsBuilder().GetSettings();
        }

        public XmlSerializerSettings(
            INameProvider nameProvider, 
            IPrimitiveProvider primitiveProvider, 
            ICollectionProvider collectionProvider, 
            IComplexProvider complexProvider, 
            ICustomProvider customProvider)
        {
            this.nameProvider = nameProvider;
            this.primitiveProvider = primitiveProvider;
            this.collectionProvider = collectionProvider;
            this.complexProvider = complexProvider;
            this.customProvider = customProvider;
        }

        public INameProvider NameProvider
        {
            get { return nameProvider; }
        }

        public IPrimitiveProvider PrimitiveProvider
        {
            get { return primitiveProvider; }
        }

        public ICollectionProvider CollectionProvider
        {
            get { return collectionProvider; }
        }

        public IComplexProvider ComplexProvider
        {
            get { return complexProvider; }
        }

        public static XmlSerializerSettings Default
        {
            get { return @default; }
        }

        public ICustomProvider CustomProvider
        {
            get { return customProvider; }
        }
    }
}