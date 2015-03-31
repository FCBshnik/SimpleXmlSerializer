using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents info how to serialize 'primitive' type.
    /// </summary>
    public class PrimitiveTypeDescription
    {
        public PrimitiveTypeDescription(IPrimitiveSerializer serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            Serializer = serializer;
        }

        /// <summary>
        /// Gets <see cref="IPrimitiveSerializer"/> for current 'primitive' type.
        /// </summary>
        public IPrimitiveSerializer Serializer { get; private set; }
    }
}