using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class LongSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public LongSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (long)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return long.Parse(value, formatProvider);
        }
    }
}