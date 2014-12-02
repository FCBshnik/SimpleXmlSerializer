using System;
using System.Collections;

namespace SimpleXmlSerializer.Core
{
    public class ArrayCollectionNodeProvider : ICollectionNodeProvider
    {
        public bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription)
        {
            if (type.IsArray)
            {
                collectionDescription = GetArrayDescription(type);
                return true;
            }

            collectionDescription = null;
            return false;
        }

        private static CollectionNodeDescription GetArrayDescription(Type collectionType)
        {
            var itemType = collectionType.GetElementType();
            return new CollectionNodeDescription(itemType, items => CreateArray(items, itemType));
        }

        private static object CreateArray(ICollection items, Type itemType)
        {
            var value = Array.CreateInstance(itemType, items.Count);
            items.CopyTo(value, 0);
            return value;
        }
    }
}