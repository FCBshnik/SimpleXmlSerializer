using System;

namespace SimpleXmlSerializer.Core
{
    public interface IPrimitiveNodeProvider
    {
        bool TryGetPrimitiveDescription(Type type, out PrimitiveNodeDescription primitiveDescription);
    }
}