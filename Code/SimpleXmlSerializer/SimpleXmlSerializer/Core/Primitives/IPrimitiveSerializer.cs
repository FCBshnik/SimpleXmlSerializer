namespace SimpleXmlSerializer.Core
{
    public interface IPrimitiveSerializer
    {
        string Serialize(object value);
        
        object Deserialize(string value);
    }
}