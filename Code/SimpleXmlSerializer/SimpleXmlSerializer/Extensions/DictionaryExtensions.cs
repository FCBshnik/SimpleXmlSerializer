using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Extensions
{
    internal static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source)
        {
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (source == null)
                throw new ArgumentNullException("source");

            foreach (var key in destination.Keys)
            {
                source[key] = destination[key];
            }

            return source;
        }
    }
}