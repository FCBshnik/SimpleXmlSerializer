using System;
using System.IO;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public class SimpleXmlSerializerAdapter : IXmlSerializerAdapter
    {
        private readonly Type serializedType;
        private readonly XmlSerializer xmlSerializer;

        public SimpleXmlSerializerAdapter(Type serializedType, XmlSerializer xmlSerializer)
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
            xmlSerializer.Serialize(obj, outputStream);
        }

        public object Deserialize(Stream inputStream)
        {
            return xmlSerializer.Deserialize(serializedType, inputStream);
        }
    }
}