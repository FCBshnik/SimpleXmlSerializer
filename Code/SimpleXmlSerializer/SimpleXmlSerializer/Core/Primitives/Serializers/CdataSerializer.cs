using System;
using System.Xml;

namespace SimpleXmlSerializer.Core.Serializers
{
    public class CdataSerializer : IPrimitiveSerializer
    {
        private readonly IPrimitiveSerializer serializer;

        public CdataSerializer(IPrimitiveSerializer serializer)
        {
            if (serializer == null) 
                throw new ArgumentNullException("serializer");

            this.serializer = serializer;
        }

        public string Serialize(object value)
        {
            var serializedValue = serializer.Serialize(value);
            var xmlDocument = new XmlDocument();
            var cdata = xmlDocument.CreateCDataSection(serializedValue);
            return cdata.OuterXml;
        }

        public object Deserialize(string serializedValue)
        {
            return serializer.Deserialize(serializedValue);
        }
    }
}