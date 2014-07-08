using System;
using System.IO;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public class DataContractSerializerAdapter : IXmlSerializerAdapter
    {
        private readonly Type serializedType;
        private readonly DataContractSerializer serializer;

        public DataContractSerializerAdapter(Type serializedType, DataContractSerializer serializer)
        {
            this.serializedType = serializedType;
            this.serializer = serializer;
        }

        public string Name
        {
            get { return serializer.GetType().FullName; }
        }

        public Type SerializedType
        {
            get { return serializedType; }
        }

        public void Serialize(object obj, Stream outputStream)
        {
            serializer.WriteObject(outputStream, obj);
        }

        public object Deserialize(Stream inputStream)
        {
            return serializer.ReadObject(inputStream);
        }
    }
}