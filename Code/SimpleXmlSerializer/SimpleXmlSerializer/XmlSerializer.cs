using System;
using System.Xml;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer
{
    public class XmlSerializer
    {
        private readonly XmlSerializerSettings settings;
        private readonly NodeDetector nodeDetector;
        
        public XmlSerializer() : this(new XmlSerializerSettingsBuilder().GetSettings())
        {
        }

        public XmlSerializer(XmlSerializerSettings settings)
        {
            Preconditions.NotNull(settings, "settings");

            this.settings = settings;
            nodeDetector = new NodeDetector(settings);
        }

        public void Serialize(object obj, XmlWriter xmlWriter)
        {
            Preconditions.NotNull(obj, "obj");
            Preconditions.NotNull(xmlWriter, "xmlWriter");

            var visitor = new SerializationVisitor(xmlWriter, nodeDetector);
            visitor.Visit(obj);
        }

        public object Deserialize(Type type, XmlReader xmlReader)
        {
            Preconditions.NotNull(type, "type");
            Preconditions.NotNull(xmlReader, "xmlReader");

            var visitor = new DeserializationVisitor(xmlReader, nodeDetector);
            return visitor.Visit(type);
        }
    }
}