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
        private readonly ICompositeTypeProvider compositeProvider;

        public XmlSerializerSettings(
            INameProvider nameProvider, 
            IPrimitiveTypeProvider primitiveProvider, 
            ICollectionTypeProvider collectionProvider, 
            ICompositeTypeProvider compositeProvider)
        {
            this.nameProvider = nameProvider;
            this.primitiveProvider = primitiveProvider;
            this.collectionProvider = collectionProvider;
            this.compositeProvider = compositeProvider;
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

        public ICompositeTypeProvider CompositeProvider
        {
            get { return compositeProvider; }
        }
    }
}