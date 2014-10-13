namespace SimpleXmlSerializer.Core
{
    public class CustomNodeDescription
    {
        public CustomNodeDescription(ICustomSerializer serializer)
        {
            Serializer = serializer;
        }

        public ICustomSerializer Serializer { get; private set; }
    }
}