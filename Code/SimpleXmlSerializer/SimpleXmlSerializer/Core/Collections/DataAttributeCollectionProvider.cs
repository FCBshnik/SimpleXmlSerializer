using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Extensions;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize collection types based on <see cref="CollectionDataContractAttribute"/> attribute.
    /// </summary>
    public class DataAttributeCollectionProvider : ICollectionTypeProvider
    {
        public bool TryGetDescription(Type type, out CollectionTypeDescription collectionDescription)
        {
            var collectionDataContractAttribute = type.FindAttribute<CollectionDataContractAttribute>();
            if (collectionDataContractAttribute != null)
            {
                if (TypeUtils.ImplementsGenericInterface(type, typeof(IDictionary<,>)))
                {
                    var dictionaryType = TypeUtils.GetImplementedGenericInterface(type, typeof(IDictionary<,>));
                    collectionDescription = GetDictionaryDescription(dictionaryType, type);
                    return true;
                }

                if (TypeUtils.ImplementsGenericInterface(type, typeof(ICollection<>)))
                {
                    var collectionType = TypeUtils.GetImplementedGenericInterface(type, typeof(ICollection<>));
                    collectionDescription = GetCollectionDescription(collectionType, type);
                    return true;
                }

                throw new SerializationException(string.Format("Can not serialize type '{0}' as collection", type));
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionTypeDescription GetCollectionDescription(Type collectionType, Type originalType)
        {
            var itemType = collectionType.GetGenericArguments()[0];
            return new CollectionTypeDescription(itemType, items => CreateCollection(items, originalType));
        }

        private static CollectionTypeDescription GetDictionaryDescription(Type dictionaryType, Type originalType)
        {
            var genericArguments = dictionaryType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionTypeDescription(itemType, items => CreateDictionary(items, genericArguments, originalType));
        }

        private static object CreateCollection(IEnumerable items, Type collectionType)
        {
            var value = (IList)Activator.CreateInstance(collectionType);
            value.AddRange(items);
            return value;
        }

        private static object CreateDictionary(IEnumerable items, Type[] genericArguments, Type originalType)
        {
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            var collectionType = typeof(ICollection<>).MakeGenericType(itemType);
            var value = Activator.CreateInstance(originalType);
            var add = collectionType.GetMethod("Add", new[] { itemType });
            foreach (var item in items)
            {
                add.Invoke(value, new[] { item });
            }

            return value;
        }
    }
}