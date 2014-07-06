using System.Xml;

namespace SimpleXmlSerializer.Core
{
    public interface ICustomNodeSerializer
    {
        void Serialize(object value, XmlWriter xmlWriter);

        object Deserialize(XmlReader xmlReader);
    }
}