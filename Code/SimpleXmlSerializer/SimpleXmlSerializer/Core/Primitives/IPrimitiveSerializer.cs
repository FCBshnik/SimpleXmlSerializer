namespace SimpleXmlSerializer.Core
{
    /// <summary>
    /// Responsible to serialize/deserialize object to/from string.
    /// Used to specify serialization of primitive type.
    /// </summary>
    public interface IPrimitiveSerializer
    {
        string Serialize(object value);
        
        object Deserialize(string value);
    }
}