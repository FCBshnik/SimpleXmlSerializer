using System;
using System.IO;
using System.Xml;
using YAXLib;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
// ReSharper disable InconsistentNaming
    public class YAXLibXmlSerializerAdapter : IXmlSerializerAdapter
// ReSharper restore InconsistentNaming
    {
        private readonly Type serializedType;
        private readonly YAXSerializer serializer;

        public YAXLibXmlSerializerAdapter(Type serializedType)
        {
            this.serializedType = serializedType;
            serializer = new YAXSerializer(serializedType);
        }

        public string Name
        {
            get { return "YAX"; }
        }

        public Type SerializedType
        {
            get { return serializedType; }
        }

        public void Serialize(object obj, Stream outputStream)
        {
            using (var xmlWriter = XmlWriter.Create(outputStream))
            {
                serializer.Serialize(obj, xmlWriter);
            }
        }

        public object Deserialize(Stream inputStream)
        {
            using (var xmlReader = XmlReader.Create(inputStream))
            {
                return serializer.Deserialize(xmlReader);
            }
        }
    }
}