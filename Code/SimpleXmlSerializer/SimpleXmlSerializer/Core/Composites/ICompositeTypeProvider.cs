using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize composite types.
    /// </summary>
    public interface ICompositeTypeProvider
    {
        /// <summary>
        /// Provides info how to serialize composite types.
        /// </summary>
        bool TryGetDescription(Type type, out CompositeTypeDescription description);
    }
}