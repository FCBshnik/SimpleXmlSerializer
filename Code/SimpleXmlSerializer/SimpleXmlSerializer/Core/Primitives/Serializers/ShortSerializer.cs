using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class ShortSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public ShortSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (short)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return short.Parse(value, formatProvider);
        }
    }
}