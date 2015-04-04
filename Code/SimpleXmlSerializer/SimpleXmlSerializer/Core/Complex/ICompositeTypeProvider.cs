using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize composite types.
    /// </summary>
    public interface ICompositeTypeProvider
    {
        bool TryGetDescription(Type type, out CompositeTypeDescription description);
    }
}