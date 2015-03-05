namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Represents information how to serialize custom type.
    /// </summary>
    public class CustomNodeDescription
    {
        public CustomNodeDescription(ICustomSerializer serializer)
        {
            Serializer = serializer;
        }

        public ICustomSerializer Serializer { get; private set; }
    }
}