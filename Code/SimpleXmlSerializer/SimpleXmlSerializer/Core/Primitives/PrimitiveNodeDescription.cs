namespace SimpleXmlSerializer.Core
{
    public class PrimitiveNodeDescription
    {
        public PrimitiveNodeDescription(IPrimitiveSerializer serializer)
        {
            Serializer = serializer;
        }

        public IPrimitiveSerializer Serializer { get; private set; }
    }
}