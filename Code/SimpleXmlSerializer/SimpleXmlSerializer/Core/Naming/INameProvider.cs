using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core.Naming
{
    public interface INameProvider
    {
        NodeName GetNodeName(Type type);

        NodeName GetNodeName(PropertyInfo propertyInfo);
    }
}