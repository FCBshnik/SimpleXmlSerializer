using System;

namespace SimpleXmlSerializer.Core.Custom
{
    public interface ICustomProvider
    {
        bool TryGetCustomSerializer(Type type, out ICustomSerializer serializer);
    }
}