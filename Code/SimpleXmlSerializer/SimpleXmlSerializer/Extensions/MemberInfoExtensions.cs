using System;
using System.Linq;
using System.Reflection;

namespace SimpleXmlSerializer.Extensions
{
    internal static class MemberInfoExtensions
    {
        public static TAttribute FindAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            return memberInfo.GetCustomAttributes(inherited).OfType<TAttribute>().FirstOrDefault();
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            return memberInfo.FindAttribute<TAttribute>(inherited) != null;
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            var attribute = memberInfo.FindAttribute<TAttribute>();
            if (attribute == null)
                throw new InvalidOperationException(string.Format("Member '{0}' does not have '{1}' attribute", memberInfo, typeof(TAttribute)));

            return attribute;
        }
    }
}