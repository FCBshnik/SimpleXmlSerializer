using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettings
    {
        private static readonly XmlSerializerSettings @default;

        private readonly INameProvider nameProvider;

        private readonly IPrimitiveProvider primitiveProvider;

        private readonly ICollectionNodeProvider collectionProvider;

        private readonly IComplexNodeProvider complexProvider;

        private readonly ICustomNodeProvider customProvider;

        static XmlSerializerSettings()
        {
            @default = new XmlSerializerSettingsBuilder().GetSettings();
        }

        public XmlSerializerSettings(
            INameProvider nameProvider, 
            IPrimitiveProvider primitiveProvider, 
            ICollectionNodeProvider collectionProvider, 
            IComplexNodeProvider complexProvider, 
            ICustomNodeProvider customProvider)
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

        public ICollectionNodeProvider CollectionProvider
        {
            get { return collectionProvider; }
        }

        public IComplexNodeProvider ComplexProvider
        {
            get { return complexProvider; }
        }

        public static XmlSerializerSettings Default
        {
            get { return @default; }
        }

        public ICustomNodeProvider CustomProvider
        {
            get { return customProvider; }
        }
    }
}