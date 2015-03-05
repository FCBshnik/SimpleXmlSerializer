using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Defines which types should be serialized as collection.
    /// </summary>
    public class CollectionNodeProvider : ICollectionNodeProvider
    {
        // todo: non-generic collection
        private static readonly HashSet<Type> collectionTypes = new HashSet<Type>
            {
                typeof(IEnumerable<>),
                typeof(ICollection<>),
                typeof(Collection<>),
                typeof(IList<>),
                typeof(List<>)
            };

        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (collectionTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    collectionDescription = GetCollectionDescription(type);
                    return true;
                }
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionNodeDescription GetCollectionDescription(Type collectionType)
        {
            var itemType = collectionType.GetGenericArguments()[0];
            return new CollectionNodeDescription(itemType, items => CreateList(items, itemType));
        }

        private static object CreateList(ICollection items, Type itemType)
        {
            var value = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new []{ itemType }));
            value.AddRange(items);
            return value;
        }
    }
}