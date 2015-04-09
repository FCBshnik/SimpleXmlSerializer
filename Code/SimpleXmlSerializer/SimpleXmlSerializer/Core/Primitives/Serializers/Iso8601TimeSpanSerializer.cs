using System;
using System.Xml;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class Iso8601TimeSpanSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var timeSpan = (TimeSpan)value;

            return XmlConvert.ToString(timeSpan);
        }

        public object Deserialize(string serializedValue)
        {
            return XmlConvert.ToTimeSpan(serializedValue);
        }
    }
}