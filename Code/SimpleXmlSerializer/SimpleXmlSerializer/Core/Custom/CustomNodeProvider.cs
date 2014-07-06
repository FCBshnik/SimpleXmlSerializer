using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CustomNodeProvider : ICustomNodeProvider
    {
        private readonly Dictionary<Type, ICustomNodeSerializer> serializers = new Dictionary<Type, ICustomNodeSerializer>();

        public bool TryGetSerializer(Type type, out ICustomNodeSerializer serializer)
        {
            return serializers.TryGetValue(type, out serializer);
        }

        public void AddSerializer(Type type, ICustomNodeSerializer serializer)
        {
            serializers[type] = serializer;
        }
    }
}