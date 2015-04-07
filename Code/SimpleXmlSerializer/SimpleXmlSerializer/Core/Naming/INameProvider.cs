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
        /// Provides name based on information about <see cref="PropertyInfo"/>.
        /// </summary>
        NodeName GetNodeName(PropertyInfo propertyInfo);

        /// <summary>
        /// Provides name based on information about <see cref="Type"/>.
        /// </summary>
        NodeName GetNodeName(Type type);
    }
}