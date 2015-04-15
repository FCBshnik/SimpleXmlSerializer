using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Returns properties of first <see cref="IPropertiesProvider"/> 
    /// which returns not empty list of properties.
    /// </summary>
    public class ChainedPropertiesProvider : IPropertiesProvider
    {
        private readonly IEnumerable<IPropertiesProvider> providers;

        public ChainedPropertiesProvider(IEnumerable<IPropertiesProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            foreach (var provider in providers)
            {
                var properties = provider.GetProperties(type).ToList();
                if (properties.Any())
                {
                    return properties;
                }
            }

            return Enumerable.Empty<PropertyInfo>();
        }
    }
}