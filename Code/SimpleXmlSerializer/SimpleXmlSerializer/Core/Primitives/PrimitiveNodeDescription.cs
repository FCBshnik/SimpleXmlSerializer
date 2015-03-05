namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents information how to serialize primitive type.
    /// </summary>
    public class PrimitiveNodeDescription
    {
        public PrimitiveNodeDescription(IPrimitiveSerializer serializer)
        {
            Serializer = serializer;
        }

        public IPrimitiveSerializer Serializer { get; private set; }
    }
}