using System;

namespace SimpleXmlSerializer.Core
{
    public interface IPrimitiveNodeProvider
    {
        bool TryGetDescription(Type type, out PrimitiveNodeDescription primitiveDescription);
    }
}