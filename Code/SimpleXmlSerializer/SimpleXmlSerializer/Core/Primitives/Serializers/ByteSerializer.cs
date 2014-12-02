using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class ByteSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public ByteSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (byte)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return byte.Parse(value, formatProvider);
        }
    }
}