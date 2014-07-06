using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public class ComplexNodeDescription
    {
        public ComplexNodeDescription(IEnumerable<PropertyInfo> properties, Func<IDictionary<PropertyInfo, object>, object> factory)
        {
            Properties = properties;
            Factory = factory;
        }

        public IEnumerable<PropertyInfo> Properties { get; private set; }

        public Func<IDictionary<PropertyInfo, object>, object> Factory { get; private set; }
    }
}