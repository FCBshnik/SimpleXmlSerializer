using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="TimeSpan"/> to string and vice versa.
    /// </summary>
    public class TimeSpanSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var value = (TimeSpan)obj;

            return value.ToString();
        }

        public object Deserialize(string serializedValue)
        {
            return TimeSpan.Parse(serializedValue);
        }
    }
}