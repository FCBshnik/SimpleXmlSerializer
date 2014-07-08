using System;
using System.IO;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    internal interface IXmlSerializerAdapter
    {
        string Name { get; }

        Type SerializedType { get; }

        void Serialize(object obj, Stream outputStream);

        object Deserialize(Stream inputStream);
    }
}