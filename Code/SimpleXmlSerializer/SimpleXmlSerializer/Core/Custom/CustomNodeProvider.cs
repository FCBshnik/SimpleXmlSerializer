using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Core
{
    public class CustomNodeProvider : ICustomNodeProvider
    {
        private readonly Dictionary<Type, CustomNodeDescription> descriptions = new Dictionary<Type, CustomNodeDescription>();

        public void AddSerializer(Type type, ICustomNodeSerializer serializer)
        {
            descriptions[type] = new CustomNodeDescription(serializer);
        }

        public bool TryGetDescription(Type type, out CustomNodeDescription description)
        {
            return descriptions.TryGetValue(type, out description);
        }
    }
}