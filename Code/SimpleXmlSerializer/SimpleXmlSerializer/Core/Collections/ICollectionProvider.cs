using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICollectionProvider
    {
        bool TryGetCollectionDescription(Type valueType, out CollectionDescription collectionDescription);
    }
}