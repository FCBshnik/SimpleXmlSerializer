namespace SimpleXmlSerializer.Core.Primitives.Serializers
{
    public class BoolSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var value = (bool)obj;

            return value.ToString();
        }

        public object Deserialize(string value)
        {
            return bool.Parse(value);
        }
    }
}