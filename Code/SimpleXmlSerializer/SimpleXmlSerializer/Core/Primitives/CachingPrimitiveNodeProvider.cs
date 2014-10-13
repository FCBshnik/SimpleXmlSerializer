using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CachingPrimitiveNodeProvider : IPrimitiveNodeProvider
    {
        private readonly Dictionary<Type, PrimitiveNodeDescription> cache = new Dictionary<Type, PrimitiveNodeDescription>();
        private readonly IPrimitiveNodeProvider cached;

        public CachingPrimitiveNodeProvider(IPrimitiveNodeProvider cached)
        {
            this.cached = cached;
        }

        public bool TryGetDescription(Type type, out PrimitiveNodeDescription primitiveDescription)
        {
            if (cache.ContainsKey(type))
            {
                primitiveDescription = cache[type];
                return true;
            }

            var result = cached.TryGetDescription(type, out primitiveDescription);
            if (result)
            {
                cache[type] = primitiveDescription;
            }

            return result;
        }
    }
}