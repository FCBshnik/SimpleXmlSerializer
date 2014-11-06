using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public class CachingNameProvider : INameProvider
    {
        private readonly Dictionary<Type, NodeName> cacheByType = new Dictionary<Type, NodeName>();
        private readonly Dictionary<PropertyInfo, NodeName> cacheByPropertyInfo = new Dictionary<PropertyInfo, NodeName>();
        private readonly INameProvider cached;

        public CachingNameProvider(INameProvider cached)
        {
            this.cached = cached;
        }

        public NodeName GetNodeName(Type type)
        {
            if (cacheByType.ContainsKey(type))
            {
                return cacheByType[type];
            }

            var nodeName = cached.GetNodeName(type);
            cacheByType[type] = nodeName;
            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            if (cacheByPropertyInfo.ContainsKey(propertyInfo))
            {
                return cacheByPropertyInfo[propertyInfo];
            }

            var nodeName = cached.GetNodeName(propertyInfo);
            cacheByPropertyInfo[propertyInfo] = nodeName;
            return nodeName;
        }
    }
}