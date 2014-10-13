using System;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class EnumSerializer : IPrimitiveSerializer
    {
        private readonly Type type;

        public EnumSerializer(Type type)
        {
            this.type = type;
        }

        public string Serialize(object value)
        {
            var obj = (Enum) value;
            return obj.ToString();
        }

        public object Deserialize(string value)
        {
            return Enum.Parse(type, value);
        }
    }
}