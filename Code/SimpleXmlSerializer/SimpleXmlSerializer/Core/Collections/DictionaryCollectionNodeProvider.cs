using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class DictionaryCollectionNodeProvider : ICollectionNodeProvider
    {
        // todo: non-generic dictionary
        private static readonly HashSet<Type> dictionaryTypes = new HashSet<Type>
            {
                typeof(IDictionary<,>), 
                typeof(Dictionary<,>)
            };

        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (dictionaryTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    collectionDescription = GetDictionaryDescription(type);
                    return true;
                }
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionNodeDescription GetDictionaryDescription(Type dictionaryType)
        {
            var genericArguments = dictionaryType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionNodeDescription(itemType, items => CreateDictionary(items, genericArguments));
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