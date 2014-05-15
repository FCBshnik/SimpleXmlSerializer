using System;

namespace SimpleXmlSerializer.Core.Primitives
{
    public interface IPrimitiveProvider
    {
        bool TryGetPrimitiveDescription(Type type, out PrimitiveDescription primitiveDescription);
    }
}