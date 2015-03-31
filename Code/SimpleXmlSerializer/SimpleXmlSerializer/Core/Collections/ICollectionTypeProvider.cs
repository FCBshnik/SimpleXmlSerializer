using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Checks if specified type should be serialized as collection
    /// and provides info how collection should be serialized.
    /// </summary>
    public interface ICollectionTypeProvider
    {
        /// <summary>
        /// Checks if specified type should be serialized as collection
        /// and provides info how collection should be serialized.
        /// </summary>
        bool TryGetDescription(Type type, out CollectionTypeDescription collectionDescription);
    }
}