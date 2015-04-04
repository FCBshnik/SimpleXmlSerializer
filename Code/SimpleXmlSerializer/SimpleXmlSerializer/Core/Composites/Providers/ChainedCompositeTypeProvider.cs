using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class ChainedCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IEnumerable<ICompositeTypeProvider> providers;

        public ChainedCompositeTypeProvider(IEnumerable<ICompositeTypeProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetDescription(type, out description))
                {
                    return true;
                }
            }

            description = null;
            return false;
        }
    }
}