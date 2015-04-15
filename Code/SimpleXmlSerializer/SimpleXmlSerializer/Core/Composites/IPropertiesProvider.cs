using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide properties of composite type to serialize.
    /// </summary>
    public interface IPropertiesProvider
    {
        /// <summary>
        /// Returns properties of type to serialize.
        /// </summary>
        IEnumerable<PropertyInfo> GetProperties(Type type);
    }
}