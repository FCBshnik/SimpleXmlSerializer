using System;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Represents xml serializer read only settings.
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
            if (nameProvider == null) 
                throw new ArgumentNullException("nameProvider");
            if (primitiveProvider == null) 
                throw new ArgumentNullException("primitiveProvider");
            if (collectionProvider == null) 
                throw new ArgumentNullException("collectionProvider");
            if (compositeProvider == null) 
                throw new ArgumentNullException("compositeProvider");

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