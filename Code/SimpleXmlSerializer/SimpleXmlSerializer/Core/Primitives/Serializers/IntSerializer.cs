using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class IntSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public IntSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (int)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return int.Parse(value, formatProvider);
        }
    }
}