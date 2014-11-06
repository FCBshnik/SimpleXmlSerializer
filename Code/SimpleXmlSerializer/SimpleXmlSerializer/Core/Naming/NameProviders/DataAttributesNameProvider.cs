using System;
using System.Reflection;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    public class DataAttributesNameProvider : INameProvider
    {
        public NodeName GetNodeName(Type type)
        {
            var attr = type.FindAttribute<DataContractAttribute>();
            if (attr != null)
            {
                return new NodeName(attr.Name);
            }

            return NodeName.Empty;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.FindAttribute<DataMemberAttribute>();
            if (attr != null)
            {
                return new NodeName(attr.Name);
            }

            return NodeName.Empty;
        }
    }
}