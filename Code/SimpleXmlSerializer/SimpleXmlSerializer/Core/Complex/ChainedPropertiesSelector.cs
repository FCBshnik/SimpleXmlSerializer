using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Returns properties of first <see cref="IPropertiesSelector"/> 
    /// which returns  not empty list of properties.
    /// </summary>
    public class ChainedPropertiesSelector : IPropertiesSelector
    {
        private readonly IEnumerable<IPropertiesSelector> selectors;

        public ChainedPropertiesSelector(IEnumerable<IPropertiesSelector> selectors)
        {
            if (selectors == null) 
                throw new ArgumentNullException("selectors");

            this.selectors = selectors;
        }

        public IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            foreach (var selector in selectors)
            {
                var properties = selector.SelectProperties(type).ToList();
                if (properties.Any())
                {
                    return properties;
                }
            }

            return Enumerable.Empty<PropertyInfo>();
        }
    }
}