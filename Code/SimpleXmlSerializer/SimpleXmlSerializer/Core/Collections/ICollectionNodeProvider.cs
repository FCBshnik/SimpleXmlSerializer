using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Checks if specified type should be serialized as collection
    /// and provides info how collection should be serialized.
    /// </summary>
    public interface ICollectionNodeProvider
    {
        bool TryGetDescription(Type type, out CollectionNodeDescription collectionDescription);
    }
}