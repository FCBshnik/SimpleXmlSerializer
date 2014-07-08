using System;
using System.IO;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public class NetXmlSerializerAdapter : IXmlSerializerAdapter
    {
        private readonly Type serializedType;
        private readonly System.Xml.Serialization.XmlSerializer xmlSerializer;

        public NetXmlSerializerAdapter(Type serializedType, System.Xml.Serialization.XmlSerializer xmlSerializer)
        {
            this.xmlSerializer = xmlSerializer;
            this.serializedType = serializedType;
        }

        public string Name
        {
            get { return xmlSerializer.GetType().FullName; }
        }

        public Type SerializedType
        {
            get { return serializedType; }
        }

        public void Serialize(object obj, Stream outputStream)
        {
            xmlSerializer.Serialize(outputStream, obj);
        }

        public object Deserialize(Stream inputStream)
        {
            return xmlSerializer.Deserialize(inputStream);
        }
    }
}