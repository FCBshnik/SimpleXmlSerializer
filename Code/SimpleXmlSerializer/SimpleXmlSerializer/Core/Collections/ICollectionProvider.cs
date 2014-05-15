using System;

namespace SimpleXmlSerializer.Core.Collections
{
    public interface ICollectionProvider
    {
        bool TryGetCollectionDescription(Type valueType, out CollectionDescription collectionDescription);
    }
}