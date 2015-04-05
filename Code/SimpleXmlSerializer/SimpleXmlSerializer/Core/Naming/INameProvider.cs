using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide xml element and attribute names for serialization process.
    /// </summary>
    public interface INameProvider
    {
        /// <summary>
        /// Provides xml element or attribute name by property info.
        /// </summary>
        NodeName GetNodeName(PropertyInfo propertyInfo);

        /// <summary>
        /// Provides xml element or attribute name by type.
        /// </summary>
        NodeName GetNodeName(Type type);
    }
}