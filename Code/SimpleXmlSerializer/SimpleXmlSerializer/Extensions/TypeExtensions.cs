using System;
using System.Linq;

namespace SimpleXmlSerializer
{
    internal static class TypeExtensions
    {
        public static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            return FindImplementedGenericInterface(type, interfaceType) != null;
        }

        public static Type FindImplementedGenericInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            var interfaces = type.GetInterfaces();

            if (interfaceType.IsGenericTypeDefinition)
            {
                var genericInterfaces = interfaces.Where(t => t.IsGenericType).Select(t => t.GetGenericTypeDefinition());
                return genericInterfaces.FirstOrDefault(i => i == interfaceType);
            }

            return interfaces.FirstOrDefault(t => t == interfaceType);
        }

        public static Type GetImplementedGenericInterface(this Type type, Type genericTypeDefinitionInterface)
        {
            var interfaces = type.GetInterfaces();
            return interfaces
                .Where(t => t.IsGenericType)
                .FirstOrDefault(t => t.GetGenericTypeDefinition() == genericTypeDefinitionInterface);
        }
    }
}