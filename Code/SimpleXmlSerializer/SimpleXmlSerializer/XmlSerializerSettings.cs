using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    public class XmlSerializerSettings
    {
        private readonly INameProvider nameProvider;
        private readonly IPrimitiveNodeProvider primitiveProvider;
        private readonly ICollectionNodeProvider collectionProvider;
        private readonly IComplexNodeProvider complexProvider;
        private readonly ICustomNodeProvider customProvider;

        public XmlSerializerSettings(
            INameProvider nameProvider, 
            IPrimitiveNodeProvider primitiveProvider, 
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

        public IPrimitiveNodeProvider PrimitiveProvider
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

        public ICustomNodeProvider CustomProvider
        {
            get { return customProvider; }
        }
    }
}