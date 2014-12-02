using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class DateTimeOffsetSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var value = (DateTimeOffset)obj;
            return value.ToUniversalTime().ToString("o");
        }

        public object Deserialize(string value)
        {
            return DateTimeOffset.Parse(value).ToUniversalTime();
        }
    }
}