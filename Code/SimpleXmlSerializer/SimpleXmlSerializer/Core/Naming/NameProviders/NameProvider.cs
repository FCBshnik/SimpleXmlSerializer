using System;
using System.Reflection;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides xml element and attribute names based on type information or property information.
    /// </summary>
    public class NameProvider : INameProvider
    {
        private readonly XmlElementName collectionElementName;
        private readonly XmlElementName collectionItemName;

        private readonly INamingConvention namingConvention;
        private readonly ICollectionTypeProvider collectionProvider;

        public NameProvider(INamingConvention namingConvention, ICollectionTypeProvider collectionProvider, XmlElementName collectionElementName, XmlElementName collectionItemName)
        {
            if (namingConvention == null) 
                throw new ArgumentNullException("namingConvention");
            if (collectionProvider == null) 
                throw new ArgumentNullException("collectionProvider");
            if (collectionElementName == null) 
                throw new ArgumentNullException("collectionElementName");
            if (collectionItemName == null) 
                throw new ArgumentNullException("collectionItemName");

            this.namingConvention = namingConvention;
            this.collectionProvider = collectionProvider;
            this.collectionElementName = collectionElementName;
            this.collectionItemName = collectionItemName;
        }

        public NodeName GetNodeName(Type type)
        {
            var elementName = namingConvention.NormalizeName(type.Name);
            var itemName = string.Empty;

            CollectionTypeDescription collectionDescription;

            // if it is collection type
            if (collectionProvider.TryGetDescription(type, out collectionDescription))
            {
                elementName = namingConvention.NormalizeName(collectionElementName.Name);
                itemName = namingConvention.NormalizeName(collectionItemName.Name);
            }
            else if (type.IsGenericType)
            {
                // if generic type, cut of 'generic' part of type name
                elementName = namingConvention.NormalizeName(type.Name.Substring(0, type.Name.IndexOf('`')));
            }

            return new NodeName(elementName, itemName);
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var elementName = namingConvention.NormalizeName(propertyInfo.Name);
            var itemName = string.Empty;

            CollectionTypeDescription collectionDescription;

            // if type of property is collection, then additionally provide item name
            if (collectionProvider.TryGetDescription(propertyInfo.PropertyType, out collectionDescription))
            {
                itemName = namingConvention.NormalizeName(collectionItemName.Name);
            }

            return new NodeName(elementName, itemName);
        }
    }
}