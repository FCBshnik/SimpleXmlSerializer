using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SimpleXmlSerializer.Extensions;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    public class DataAttributeCollectionProvider : ICollectionNodeProvider
    {
        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            var collAttr = type.FindAttribute<CollectionDataContractAttribute>();
            if (collAttr != null)
            {
                if (TypeUtils.ImplementsGenericInterface(type, typeof(IList<>)))
                {
                    var collectionType = TypeUtils.GetImplementedGenericInterface(type, typeof(ICollection<>));
                    collectionDescription = GetCollectionDescription(collectionType, type);
                    return true;
                }

                if (TypeUtils.ImplementsGenericInterface(type, typeof(IDictionary<,>)))
                {
                    var dictionaryType = TypeUtils.GetImplementedGenericInterface(type, typeof(IDictionary<,>));
                    collectionDescription = GetDictionaryDescription(dictionaryType, type);
                    return true;
                }
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionNodeDescription GetCollectionDescription(Type collectionType, Type originalType)
        {
            var itemType = collectionType.GetGenericArguments()[0];
            return new CollectionNodeDescription(itemType, items => CreateList(items, originalType));
        }

        private static object CreateList(IList items, Type collectionType)
        {
            var value = (IList)Activator.CreateInstance(collectionType);
            value.AddRange(items);
            return value;
        }

        private static CollectionNodeDescription GetDictionaryDescription(Type dictionaryType, Type originalType)
        {
            var genericArguments = dictionaryType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionNodeDescription(itemType, items => CreateDictionary(items, genericArguments, originalType));
        }

        private static object CreateDictionary(ICollection items, Type[] genericArguments, Type originalType)
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