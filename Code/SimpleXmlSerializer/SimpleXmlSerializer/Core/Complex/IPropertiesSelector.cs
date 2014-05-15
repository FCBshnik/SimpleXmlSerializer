using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core.Complex
{
    public interface IPropertiesSelector
    {
        IEnumerable<PropertyInfo> SelectProperties(Type type);
    }
}