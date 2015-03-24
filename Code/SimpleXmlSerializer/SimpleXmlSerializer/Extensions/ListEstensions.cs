using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleXmlSerializer.Extensions
{
    internal static class ListEstensions
    {
        public static void AddRange(this IList list, IEnumerable items)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (items == null) throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        public static void Prepend<TItem>(this IList<TItem> list, TItem item)
        {
            if (list == null) throw new ArgumentNullException("list");

            list.Insert(0, item);
        }
    }
}