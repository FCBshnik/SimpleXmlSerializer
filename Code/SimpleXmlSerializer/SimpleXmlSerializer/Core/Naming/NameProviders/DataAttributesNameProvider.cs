﻿using System;
using System.Reflection;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    public class DataAttributesNameProvider : INameProvider
    {
        public NodeName GetNodeName(Type type)
        {
            var collAttr = type.FindAttribute<CollectionDataContractAttribute>();
            if (collAttr != null)
            {
                return new NodeName(collAttr.Name, collAttr.ItemName);
            }

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
            var collAttr = propertyInfo.PropertyType.FindAttribute<CollectionDataContractAttribute>();

            // give precedence to DataMemberAttribute
            if (attr != null)
            {
                if (collAttr != null)
                {
                    return new NodeName(attr.Name, collAttr.ItemName);
                }

                return new NodeName(attr.Name);
            }

            if (collAttr != null)
            {
                return new NodeName(collAttr.Name, collAttr.ItemName);
            }

            return NodeName.Empty;
        }
    }
}