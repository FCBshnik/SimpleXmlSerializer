using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide properties of type which should be serialized.
    /// </summary>
    public interface IPropertiesSelector
    {
        IEnumerable<PropertyInfo> SelectProperties(Type type);
    }
}