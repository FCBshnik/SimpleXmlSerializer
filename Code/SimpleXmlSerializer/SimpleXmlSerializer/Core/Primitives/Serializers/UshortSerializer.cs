using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class UshortSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UshortSerializer(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (ushort)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string value)
        {
            return ushort.Parse(value, formatProvider);
        }
    }
}