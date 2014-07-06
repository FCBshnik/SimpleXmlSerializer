using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICollectionNodeProvider
    {
        bool TryGetDescription(Type valueType, out CollectionNodeDescription collectionDescription);
    }
}