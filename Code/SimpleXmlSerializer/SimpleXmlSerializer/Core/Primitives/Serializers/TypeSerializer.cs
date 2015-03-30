using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class TypeSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var type = (Type)value;

            return type.FullName;
        }

        public object Deserialize(string value)
        {
            return Type.GetType(value);
        }
    }
}