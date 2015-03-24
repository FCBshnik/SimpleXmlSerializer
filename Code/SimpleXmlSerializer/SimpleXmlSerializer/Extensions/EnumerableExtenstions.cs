using System.Collections;
using System.Linq;

namespace SimpleXmlSerializer.Extensions
{
    internal static class EnumerableExtenstions
    {
        public static IEnumerable SkipNulls(this IEnumerable collection)
        {
            return collection.Cast<object>().Where(item => item != null);
        }
    }
}