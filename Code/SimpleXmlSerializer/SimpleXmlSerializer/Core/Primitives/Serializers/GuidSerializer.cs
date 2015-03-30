using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class GuidSerializer : IPrimitiveSerializer
    {
        private readonly string guidFormat;
        private readonly IFormatProvider formatProvider;

        public GuidSerializer(string guidFormat, IFormatProvider formatProvider)
        {
            this.guidFormat = guidFormat;
            this.formatProvider = formatProvider;
        }

        public string Serialize(object value)
        {
            var guid = (Guid)value;

            return guid.ToString(guidFormat, formatProvider);
        }

        public object Deserialize(string value)
        {
            return string.IsNullOrEmpty(guidFormat) ? Guid.Parse(value) : Guid.ParseExact(value, guidFormat);
        }
    }
}