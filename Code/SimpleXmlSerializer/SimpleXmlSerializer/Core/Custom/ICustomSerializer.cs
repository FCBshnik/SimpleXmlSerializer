using System.Xml;

namespace SimpleXmlSerializer.Core.Custom
{
    public interface ICustomSerializer
    {
        void Serialize(object value, XmlWriter xmlWriter);

        object Deserialize(XmlReader xmlReader);
    }
}