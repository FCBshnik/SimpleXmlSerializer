using System;

namespace SimpleXmlSerializer.Core.Primitives.Serializers
{
    public class DoubleSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DoubleSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (double)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return double.Parse(value, formatProvider);
        }
    }
}