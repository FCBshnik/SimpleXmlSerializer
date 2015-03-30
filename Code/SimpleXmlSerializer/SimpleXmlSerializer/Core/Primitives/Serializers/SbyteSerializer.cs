using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class SbyteSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public SbyteSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (sbyte)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return sbyte.Parse(value, formatProvider);
        }
    }
}