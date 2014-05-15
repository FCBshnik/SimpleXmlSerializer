using System;

namespace SimpleXmlSerializer.Core.Primitives.Serializers
{
    public class FloatSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public FloatSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (float)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return float.Parse(value, formatProvider);
        }
    }
}