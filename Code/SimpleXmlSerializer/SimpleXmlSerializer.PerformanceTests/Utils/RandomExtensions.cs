using System;

namespace SimpleXmlSerializer.PerformanceTests.Utils
{
    public static class RandomExtensions
    {
        public static bool GetNextBool(this Random random)
        {
            return random.Next(2) > 0;
        }

        public static DateTime GetNextDateTime(this Random random)
        {
            return new DateTime((long) (random.NextDouble() * Math.Pow(10, 15)));
        }

        public static TimeSpan GetNextTimeSpan(this Random random)
        {
            return TimeSpan.FromMilliseconds((long) (random.NextDouble() * Math.Pow(10, 6)));
        }
    }
}