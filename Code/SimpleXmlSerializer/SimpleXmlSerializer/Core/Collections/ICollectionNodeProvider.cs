using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICollectionNodeProvider
    {
        bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription);
    }
}