using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CompositeCollectionNodeProvider : ICollectionNodeProvider
    {
        private readonly IEnumerable<ICollectionNodeProvider> providers;

        public CompositeCollectionNodeProvider(IEnumerable<ICollectionNodeProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetDescription(type, out collectionDescription))
                {
                    return true;
                }
            }

            collectionDescription = null;
            return false;
        }
    }
}