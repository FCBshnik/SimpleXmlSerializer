using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide list of properties of composite type to serialize.
    /// </summary>
    public interface IPropertiesSelector
    {
        /// <summary>
        /// Returns list of properties of composite type.
        /// </summary>
        IEnumerable<PropertyInfo> SelectProperties(Type type);
    }
}