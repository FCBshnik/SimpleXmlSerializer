using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Checks if specified type should be serialized as 'primitive'
    /// and provides info how it should be serialized.
    /// </summary>
    public interface IPrimitiveTypeProvider
    {
        /// <summary>
        /// Checks if specified type should be serialized as 'primitive'
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(Type type, out PrimitiveTypeDescription primitiveDescription);
    }
}