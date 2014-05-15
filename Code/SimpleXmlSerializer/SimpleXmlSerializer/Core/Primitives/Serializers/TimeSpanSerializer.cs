using System;

namespace SimpleXmlSerializer.Core.Primitives.Serializers
{
    public class TimeSpanSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var value = (TimeSpan)obj;

            return value.ToString();
        }

        public object Deserialize(string value)
        {
            return TimeSpan.Parse(value);
        }
    }
}