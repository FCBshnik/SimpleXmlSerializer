using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    public class CollectionNodeProvider : ICollectionNodeProvider
    {
        private static readonly HashSet<Type> collectionTypes = new HashSet<Type>
            {
                typeof(IEnumerable), typeof(IEnumerable<>), 
                typeof(ICollection), typeof(ICollection<>), typeof(Collection<>),
                typeof(IList), typeof(IList<>), typeof(List<>)
            };

        private static readonly HashSet<Type> dictionaryTypes = new HashSet<Type>
            {
                typeof(IDictionary), typeof(IDictionary<,>), typeof(Dictionary<,>)
            };

        public bool TryGetDescription(Type valueType, out CollectionNodeDescription collectionDescription)
        {
            if (valueType.IsGenericType)
            {
                var genericTypeDefinition = valueType.GetGenericTypeDefinition();

                // handle collections
                if (collectionTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    collectionDescription = GetCollectionDescription(valueType);
                    return true;
                }

                // handle dictionaries
                if (dictionaryTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    collectionDescription = GetDictionaryDescription(valueType);
                    return true;
                }
            }

            // handle arrays
            if (valueType.IsArray)
            {
                collectionDescription = GetArrayDescription(valueType);
                return true;
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionNodeDescription GetCollectionDescription(Type collectionType)
        {
            var itemType = collectionType.GetGenericArguments()[0];
            return new CollectionNodeDescription(itemType, items => CreateList(items, itemType));
        }

        private static CollectionNodeDescription GetArrayDescription(Type collectionType)
        {
            var itemType = collectionType.GetElementType();
            return new CollectionNodeDescription(itemType, items => CreateArray(items, itemType));
        }

        private static CollectionNodeDescription GetDictionaryDescription(Type collectionType)
        {
            var genericArguments = collectionType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionNodeDescription(itemType, items => CreateDictionary(items, genericArguments));
        }

        private static object CreateArray(ICollection items, Type itemType)
        {
            var value = Array.CreateInstance(itemType, items.Count);
            items.CopyTo(value, 0);
            return value;
        }

        private static object CreateList(ICollection items, Type itemType)
        {
            var value = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new []{ itemType }));
            value.AddRange(items);
            return value;
        }

        private static object CreateDictionary(ICollection items, Type[] genericArguments)
        {
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            var type = typeof(Dictionary<,>).MakeGenericType(genericArguments);
            var collectionType = typeof(ICollection<>).MakeGenericType(itemType);
            var value = Activator.CreateInstance(type);
            var add = collectionType.GetMethod("Add", new[] { itemType });
            foreach (var item in items)
            {
                add.Invoke(value, new[] { item });
            }

            return value;
        }
    }
}