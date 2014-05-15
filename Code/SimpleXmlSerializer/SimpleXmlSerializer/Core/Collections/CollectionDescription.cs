using System;
using System.Collections;

namespace SimpleXmlSerializer.Core
{
    public class CollectionDescription
    {
        public CollectionDescription(Type itemType, Func<IList, object> factory)
        {
            Factory = factory;
            ItemType = itemType;
        }

        public Type ItemType { get; private set; }

        public Func<IList, object> Factory { get; private set; }
    }
}