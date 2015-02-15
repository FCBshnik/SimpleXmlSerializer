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
        /// Provides xml element or attribute name if property info is available.
        /// </summary>
        NodeName GetNodeName(PropertyInfo propertyInfo);

        /// <summary>
        /// Provides xml element or attribute name if only type info is available.
        /// </summary>
        NodeName GetNodeName(Type type);
    }
}