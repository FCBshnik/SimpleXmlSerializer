using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class UriSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var uri = (Uri)value;
            return uri.AbsoluteUri;
        }

        public object Deserialize(string value)
        {
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }
    }
}