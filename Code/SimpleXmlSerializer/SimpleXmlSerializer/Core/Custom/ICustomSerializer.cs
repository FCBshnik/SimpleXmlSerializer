using System.Xml;

namespace SimpleXmlSerializer.Core
{
    public interface ICustomSerializer
    {
        void Serialize(object value, XmlWriter xmlWriter);

        object Deserialize(XmlReader xmlReader);
    }
}