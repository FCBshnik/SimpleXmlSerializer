using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Checks if specified type should be serialized as composite
    /// and provides info how it should be serialized.
    /// </summary>
    public interface ICompositeTypeProvider
    {
        /// <summary>
        /// Checks if specified type should be serialized as composite
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(Type type, out CompositeTypeDescription description);
    }
}