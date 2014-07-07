using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICustomNodeProvider
    {
        bool TryGetDescription(Type type, out CustomNodeDescription description);
    }
}