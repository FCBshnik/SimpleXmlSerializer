using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICustomNodeProvider
    {
        bool TryGetSerializer(Type type, out ICustomNodeSerializer serializer);
    }
}