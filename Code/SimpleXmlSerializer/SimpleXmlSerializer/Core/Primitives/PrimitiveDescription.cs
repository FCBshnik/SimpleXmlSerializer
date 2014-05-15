namespace SimpleXmlSerializer.Core
{
    public class PrimitiveDescription
    {
        public PrimitiveDescription(IPrimitiveSerializer serializer)
        {
            Serializer = serializer;
        }

        public IPrimitiveSerializer Serializer { get; private set; }
    }
}