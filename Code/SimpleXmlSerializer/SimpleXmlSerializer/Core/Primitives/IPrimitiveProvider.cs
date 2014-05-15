using System;

namespace SimpleXmlSerializer.Core
{
    public interface IPrimitiveProvider
    {
        bool TryGetPrimitiveDescription(Type type, out PrimitiveDescription primitiveDescription);
    }
}