using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class DateTimeSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var value = (DateTime)obj;

            return value.ToUniversalTime().ToString("o");
        }

        public object Deserialize(string value)
        {
            return DateTime.Parse(value).ToUniversalTime();
        }
    }
}