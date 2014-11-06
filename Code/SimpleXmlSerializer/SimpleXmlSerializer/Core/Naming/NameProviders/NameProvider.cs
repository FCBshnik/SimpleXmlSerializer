using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    public class NameProvider : INameProvider
    {
        private const string CollectionName = "collection";
        private const string CollectionItemName = "add";

        private readonly INamingConvention namingConvention = new CamelCaseNamingConvention();

        private readonly ICollectionNodeProvider collectionProvider = new CollectionNodeProvider();

        public NodeName GetNodeName(Type type)
        {
            var name = type.Name;
            var itemName = string.Empty;

            CollectionNodeDescription collectionDescription;

            if (collectionProvider.TryGetDescription(type, out collectionDescription))
            {
                name = CollectionName;
                itemName = CollectionItemName;
            }
            else if (type.IsGenericType)
            {
                name = type.Name.Substring(0, type.Name.IndexOf('`'));
            }

            name = namingConvention.NormalizeName(name);

            return new NodeName(name, itemName);
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var name = namingConvention.NormalizeName(propertyInfo.Name);
            var itemName = string.Empty;

            CollectionNodeDescription collectionDescription;

            if (collectionProvider.TryGetDescription(propertyInfo.PropertyType, out collectionDescription))
            {
                itemName = CollectionItemName;
            }

            return new NodeName(name, itemName);
        }
    }
}