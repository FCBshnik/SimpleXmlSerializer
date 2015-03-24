using System;
using System.Linq;
using System.Reflection;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TAttribute FindAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }
            return memberInfo.GetCustomAttributes(inherited).OfType<TAttribute>().FirstOrDefault();
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            return memberInfo.FindAttribute<TAttribute>(inherited) != null;
        }
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo, bool inherited = false) where TAttribute : Attribute
        {
            var attr = memberInfo.FindAttribute<TAttribute>();
            if (attr == null)
            {
                throw new InvalidOperationException(string.Format("Member '{0}' does not have '{1}' attribute", memberInfo, typeof(TAttribute)));
            }

            return attr;
        }
    }
}