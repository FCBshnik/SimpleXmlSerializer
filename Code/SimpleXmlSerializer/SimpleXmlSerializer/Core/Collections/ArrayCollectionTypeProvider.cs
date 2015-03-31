using System;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize <see cref="Array"/> types.
    /// </summary>
    public class ArrayCollectionTypeProvider : ICollectionTypeProvider
    {
        public bool TryGetDescription(Type type, out CollectionTypeDescription collectionDescription)
        {
            if (type.IsArray)
            {
                var itemType = type.GetElementType();
                collectionDescription = new CollectionTypeDescription(itemType, items => FactoryUtils.CreateArray(items, itemType));
                return true;
            }

            collectionDescription = null;
            return false;
        }
    }
}