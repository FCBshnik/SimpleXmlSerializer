using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// The implementation of <see cref="IPrimitiveTypeProvider"/> which just contains
    /// map of <see cref="Type"/> to corresponding <see cref="IPrimitiveSerializer"/>.
    /// </summary>
    public class PrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IDictionary<Type, IPrimitiveSerializer> serializers;

        public PrimitiveTypeProvider(IDictionary<Type, IPrimitiveSerializer> serializers)
        {
            if (serializers == null) 
                throw new ArgumentNullException("serializers");

            this.serializers = serializers;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription primitiveDescription)
        {
            IPrimitiveSerializer primitiveSerializer;
            if (serializers.TryGetValue(type, out primitiveSerializer))
            {
                primitiveDescription = new PrimitiveTypeDescription(primitiveSerializer);
                return true;
            }

            primitiveDescription = null;
            return false;
        }
    }
}