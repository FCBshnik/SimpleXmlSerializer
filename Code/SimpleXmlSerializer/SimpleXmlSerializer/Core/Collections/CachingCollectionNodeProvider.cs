using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CachingCollectionNodeProvider : ICollectionNodeProvider
    {
        private readonly Dictionary<Type, CollectionNodeDescription> cache = new Dictionary<Type,CollectionNodeDescription>();
        private readonly ICollectionNodeProvider cached;

        public CachingCollectionNodeProvider(ICollectionNodeProvider cached)
        {
            this.cached = cached;
        }

        public bool TryGetDescription(Type valueType, out CollectionNodeDescription collectionDescription)
        {
            if (cache.ContainsKey(valueType))
            {
                collectionDescription = cache[valueType];
                return true;
            }

            var result = cached.TryGetDescription(valueType, out collectionDescription);
            if (result)
            {
                cache[valueType] = collectionDescription;
            }

            return result;
        }
    }
}