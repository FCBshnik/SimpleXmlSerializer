using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CachingComplexNodeProvider : IComplexNodeProvider
    {
        private readonly Dictionary<Type, ComplexNodeDescription> cache = new Dictionary<Type, ComplexNodeDescription>();
        private readonly IComplexNodeProvider cached;

        public CachingComplexNodeProvider(IComplexNodeProvider cached)
        {
            this.cached = cached;
        }

        public ComplexNodeDescription GetDescription(Type type)
        {
            if (cache.ContainsKey(type))
            {
                return cache[type];
            }

            var complexNodeDescription = cached.GetDescription(type);
            cache[type] = complexNodeDescription;
            return complexNodeDescription;
        }
    }
}