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
        /// Provides xml element or attribute name based on <see cref="PropertyInfo"/>.
        /// </summary>
        NodeName GetNodeName(PropertyInfo propertyInfo);

        /// <summary>
        /// Provides xml element or attribute name based on <see cref="Type"/>.
        /// </summary>
        NodeName GetNodeName(Type type);
    }
}