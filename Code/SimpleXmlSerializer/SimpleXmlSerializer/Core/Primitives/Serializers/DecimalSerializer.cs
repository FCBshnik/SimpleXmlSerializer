using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class DecimalSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DecimalSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (decimal)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return decimal.Parse(value, formatProvider);
        }
    }
}