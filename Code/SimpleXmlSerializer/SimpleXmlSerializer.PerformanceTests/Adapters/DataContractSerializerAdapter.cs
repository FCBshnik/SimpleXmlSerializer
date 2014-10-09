using System;
using System.IO;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public class DataContractSerializerAdapter : IXmlSerializerAdapter
    {
        private readonly Type serializedType;
        private readonly DataContractSerializer serializer;

        public DataContractSerializerAdapter(Type serializedType)
        {
            this.serializedType = serializedType;
            this.serializer = new DataContractSerializer(serializedType);
        }

        public string Name
        {
            get { return "DataContract"; }
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