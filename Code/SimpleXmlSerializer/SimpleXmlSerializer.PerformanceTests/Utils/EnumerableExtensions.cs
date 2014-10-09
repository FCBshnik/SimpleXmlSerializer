using System.Collections.Generic;
using System.Linq;

namespace SimpleXmlSerializer.PerformanceTests.Utils
{
    public static class EnumerableExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return pairs.ToDictionary(p => p.Key, p => p.Value);
        }
    }
}