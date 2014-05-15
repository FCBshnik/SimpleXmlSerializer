using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public interface INameProvider
    {
        NodeName GetNodeName(Type type);

        NodeName GetNodeName(PropertyInfo propertyInfo);
    }
}