using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class UintSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UintSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (uint)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return uint.Parse(value, formatProvider);
        }
    }
}