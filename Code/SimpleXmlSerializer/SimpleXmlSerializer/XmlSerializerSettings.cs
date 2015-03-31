using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Contains serialization settings. This class is read only.
    /// </summary>
    public class XmlSerializerSettings
    {
        private readonly INameProvider nameProvider;
        private readonly IPrimitiveTypeProvider primitiveProvider;
        private readonly ICollectionTypeProvider collectionProvider;
        private readonly IComplexNodeProvider complexProvider;

        public XmlSerializerSettings(
            INameProvider nameProvider, 
            IPrimitiveTypeProvider primitiveProvider, 
            ICollectionTypeProvider collectionProvider, 
            IComplexNodeProvider complexProvider)
        {
            this.nameProvider = nameProvider;
            this.primitiveProvider = primitiveProvider;
            this.collectionProvider = collectionProvider;
            this.complexProvider = complexProvider;
        }

        public INameProvider NameProvider
        {
            get { return nameProvider; }
        }

        public IPrimitiveTypeProvider PrimitiveProvider
        {
            get { return primitiveProvider; }
        }

        public ICollectionTypeProvider CollectionProvider
        {
            get { return collectionProvider; }
        }

        public IComplexNodeProvider ComplexProvider
        {
            get { return complexProvider; }
        }
    }
}