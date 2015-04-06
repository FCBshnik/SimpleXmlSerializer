using System;

namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Provides info how to serialize nullable types. Uses specified <see cref="IPrimitiveTypeProvider"/>
    /// to provide info how to serialize type underlying nullable type.
    /// </summary>
    public class NullablePrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IPrimitiveTypeProvider provider;

        public NullablePrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription primitiveDescription)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return provider.TryGetDescription(type.GetGenericArguments()[0], out primitiveDescription);
            }

            primitiveDescription = null;
            return false;
        }
    }
}