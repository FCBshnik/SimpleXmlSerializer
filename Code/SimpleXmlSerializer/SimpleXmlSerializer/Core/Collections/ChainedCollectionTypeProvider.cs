using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Picks up first <see cref="ICollectionTypeProvider"/> from collection 
    /// which knows how to serialize specified collection type.
    /// </summary>
    public class ChainedCollectionTypeProvider : ICollectionTypeProvider
    {
        private readonly IEnumerable<ICollectionTypeProvider> providers;

        public ChainedCollectionTypeProvider(IEnumerable<ICollectionTypeProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out CollectionTypeDescription collectionDescription)
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