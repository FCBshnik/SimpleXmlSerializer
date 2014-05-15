using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CustomProvider : ICustomProvider
    {
        private readonly Dictionary<Type, ICustomSerializer> serializers = new Dictionary<Type, ICustomSerializer>();

        public bool TryGetCustomSerializer(Type type, out ICustomSerializer serializer)
        {
            return serializers.TryGetValue(type, out serializer);
        }

        public void AddSerializer(Type type, ICustomSerializer serializer)
        {
            serializers[type] = serializer;
        }
    }
}