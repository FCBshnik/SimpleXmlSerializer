using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides xml element and attribute names based on type information or property information.
    /// </summary>
    public class NameProvider : INameProvider
    {
        private const string CollectionName = "collection";
        private const string CollectionItemName = "add";

        private readonly INamingConvention namingConvention;
        private readonly ICollectionNodeProvider collectionProvider;

        public NameProvider(INamingConvention namingConvention, ICollectionNodeProvider collectionProvider)
        {
            if (namingConvention == null) throw new ArgumentNullException("namingConvention");
            if (collectionProvider == null) throw new ArgumentNullException("collectionProvider");

            this.namingConvention = namingConvention;
            this.collectionProvider = collectionProvider;
        }

        public NodeName GetNodeName(Type type)
        {
            var name = type.Name;
            var itemName = string.Empty;

            CollectionNodeDescription collectionDescription;

            // if it is collection type
            if (collectionProvider.TryGetDescription(type, out collectionDescription))
            {
                name = CollectionName;
                itemName = CollectionItemName;
            }
            else if (type.IsGenericType)
            {
                // if generic type, cut of 'generic' part of type name
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

            // if type of property is collection, then additionally provide item name
            if (collectionProvider.TryGetDescription(propertyInfo.PropertyType, out collectionDescription))
            {
                itemName = CollectionItemName;
            }

            return new NodeName(name, itemName);
        }
    }
}