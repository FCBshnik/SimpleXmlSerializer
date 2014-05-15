using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public interface IPropertiesSelector
    {
        IEnumerable<PropertyInfo> SelectProperties(Type type);
    }
}