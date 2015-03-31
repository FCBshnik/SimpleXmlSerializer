using System;
using System.Collections.Generic;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize IDictionary and Dictionary types.
    /// </summary>
    public class DictionaryCollectionTypeProvider : ICollectionTypeProvider
    {
        private static readonly HashSet<Type> dictionaryTypes = new HashSet<Type>
            {
                typeof(IDictionary<,>), 
                typeof(Dictionary<,>)
            };

        public bool TryGetDescription(Type type, out CollectionTypeDescription collectionDescription)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (dictionaryTypes.Contains(genericTypeDefinition))
                {
                    collectionDescription = GetDescription(type);
                    return true;
                }
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionTypeDescription GetDescription(Type dictionaryType)
        {
            var genericArguments = dictionaryType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionTypeDescription(itemType, items => FactoryUtils.CreateDictionary(items, genericArguments[0], genericArguments[1]));
        }
    }
}