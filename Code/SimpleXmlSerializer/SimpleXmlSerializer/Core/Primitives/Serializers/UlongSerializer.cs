using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class UlongSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UlongSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (ulong)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return ulong.Parse(value, formatProvider);
        }
    }
}