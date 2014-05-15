namespace SimpleXmlSerializer.Core.Primitives
{
    public interface IPrimitiveSerializer
    {
        string Serialize(object value);
        
        object Deserialize(string value);
    }
}