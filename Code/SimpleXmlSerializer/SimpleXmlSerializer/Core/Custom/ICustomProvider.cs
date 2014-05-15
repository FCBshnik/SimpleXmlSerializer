using System;

namespace SimpleXmlSerializer.Core
{
    public interface ICustomProvider
    {
        bool TryGetCustomSerializer(Type type, out ICustomSerializer serializer);
    }
}