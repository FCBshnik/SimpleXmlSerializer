using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CachingCollectionNodeProvider : ICollectionNodeProvider
    {
        private readonly Dictionary<Type, CollectionNodeDescription> cache = new Dictionary<Type, CollectionNodeDescription>();
        private readonly ICollectionNodeProvider cached;

        public CachingCollectionNodeProvider(ICollectionNodeProvider cached)
        {
            this.cached = cached;
        }

        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            if (cache.ContainsKey(type))
            {
                collectionDescription = cache[type];
                return true;
            }

            var result = cached.TryGetDescription(type, out collectionDescription);
            if (result)
            {
                cache[type] = collectionDescription;
            }

            return result;
        }
    }
}