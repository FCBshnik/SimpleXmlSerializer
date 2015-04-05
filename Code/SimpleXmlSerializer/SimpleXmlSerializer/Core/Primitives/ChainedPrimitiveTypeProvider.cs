using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Picks up first <see cref="IPrimitiveTypeProvider"/> from collection 
    /// which knows how to serialize specified primitive type.
    /// </summary>
    public class ChainedPrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IEnumerable<IPrimitiveTypeProvider> providers;

        public ChainedPrimitiveTypeProvider(IEnumerable<IPrimitiveTypeProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription primitiveDescription)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetDescription(type, out primitiveDescription))
                {
                    return true;
                }
            }

            primitiveDescription = null;
            return false;
        }
    }
}