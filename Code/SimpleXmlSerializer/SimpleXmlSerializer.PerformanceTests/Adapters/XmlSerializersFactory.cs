using System;
using System.Collections.Generic;

namespace SimpleXmlSerializer.PerformanceTests.Adapters
{
    public static class XmlSerializersFactory
    {
        private static readonly List<Func<Type, IXmlSerializerAdapter>> factories = new List<Func<Type, IXmlSerializerAdapter>>
            {
                t => new DataContractSerializerAdapter(t),
                t => new NetXmlSerializerAdapter(t),
                t => new SimpleXmlSerializerAdapter(t),
                t => new YAXLibXmlSerializerAdapter(t),
            };

        public static IEnumerable<IXmlSerializerAdapter> CreateSerializers(Type type)
        {
            foreach (var factory in factories)
            {
                IXmlSerializerAdapter serializer = null;
                try
                {
                    serializer = factory(type);
                }
// ReSharper disable EmptyGeneralCatchClause
                catch
// ReSharper restore EmptyGeneralCatchClause
                {
                }

                if (serializer != null)
                {
                    yield return serializer;
                }
            }
        }

        public static IEnumerable<IXmlSerializerAdapter> CreateSimpleSerializer(Type type)
        {
            yield return new SimpleXmlSerializerAdapter(type);
        }
    }
}