using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to provide list of type properties to serialize.
    /// </summary>
    public interface IPropertiesSelector
    {
        IEnumerable<PropertyInfo> SelectProperties(Type type);
    }
}