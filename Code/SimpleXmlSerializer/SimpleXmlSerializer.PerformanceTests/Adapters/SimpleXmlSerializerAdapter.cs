using System;
using System.IO;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public class SimpleXmlSerializerAdapter : IXmlSerializerAdapter
    {
        private readonly Type serializedType;
        private readonly XmlSerializer xmlSerializer;

        public SimpleXmlSerializerAdapter(Type serializedType)
        {
            xmlSerializer = new XmlSerializer();
            this.serializedType = serializedType;
        }

        public string Name
        {
            get { return "Simple"; }
        }

        public Type SerializedType
        {
            get { return serializedType; }
        }

        public void Serialize(object obj, Stream outputStream)
        {
            xmlSerializer.SerializeToStream(obj, outputStream);
        }

        public object Deserialize(Stream inputStream)
        {
            return xmlSerializer.DeserializeFromStream(serializedType, inputStream);
        }
    }
}