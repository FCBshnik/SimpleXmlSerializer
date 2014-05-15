using System.Collections;

namespace SimpleXmlSerializer.Extensions
{
    public static class ListEstensions
    {
        public static void AddRange(this IList list, IEnumerable items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }
    }
}