using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleXmlSerializer.PerformanceTests.Utils
{
    public static class TimeSpanUtils
    {
        public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
        {
            return TimeSpan.FromMilliseconds(timeSpans.Sum(t => t.TotalMilliseconds));
        }

        public static TimeSpan Avg(this IEnumerable<TimeSpan> timeSpans)
        {
            var list = timeSpans.ToList();
            return TimeSpan.FromMilliseconds(list.Sum(t => t.TotalMilliseconds) / list.Count());
        }
    }
}