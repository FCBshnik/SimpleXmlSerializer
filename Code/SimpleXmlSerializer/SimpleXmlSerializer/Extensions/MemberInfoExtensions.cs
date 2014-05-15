using System;
using System.Linq;
using System.Reflection;

namespace SimpleXmlSerializer.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TAttribute FindAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(inherited).OfType<TAttribute>().FirstOrDefault();
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            return memberInfo.FindAttribute<TAttribute>(inherited) != null;
        }
    }
}