namespace SimpleXmlSerializer.Core
{
    public class CustomNodeDescription
    {
        public CustomNodeDescription(ICustomNodeSerializer serializer)
        {
            Serializer = serializer;
        }

        public ICustomNodeSerializer Serializer { get; private set; }
    }
}