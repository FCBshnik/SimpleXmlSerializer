namespace SimpleXmlSerializer.Core.Serializers
{
    public class CharSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var c = (char)obj;
            return new string(new [] { c });
        }

        public object Deserialize(string value)
        {
            return value[0];
        }
    }
}