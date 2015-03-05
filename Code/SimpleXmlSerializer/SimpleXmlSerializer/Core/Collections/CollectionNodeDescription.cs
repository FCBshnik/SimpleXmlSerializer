using System;
using System.Collections;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents information how to serialize collection type.
    /// </summary>
    public class CollectionNodeDescription
    {
        public CollectionNodeDescription(Type itemType, Func<IList, object> factory)
        {
            Factory = factory;
            ItemType = itemType;
        }

        public Type ItemType { get; private set; }

        public Func<IList, object> Factory { get; private set; }
    }
}