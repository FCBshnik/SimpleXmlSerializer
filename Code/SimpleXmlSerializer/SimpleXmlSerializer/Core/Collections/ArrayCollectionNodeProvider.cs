using System;
using System.Collections;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize array types.
    /// </summary>
    public class ArrayCollectionNodeProvider : ICollectionNodeProvider
    {
        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            if (type.IsArray)
            {
                var itemType = type.GetElementType();
                collectionDescription = new CollectionNodeDescription(itemType, items => CreateArray(items, itemType));
                return true;
            }

            collectionDescription = null;
            return false;
        }

        private static object CreateArray(ICollection items, Type itemType)
        {
            var value = Array.CreateInstance(itemType, items.Count);
            items.CopyTo(value, 0);
            return value;
        }
    }
}