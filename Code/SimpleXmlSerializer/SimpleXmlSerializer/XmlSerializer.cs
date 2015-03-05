using System;
using System.Xml;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer
{
    public class XmlSerializer
    {
        private readonly XmlSerializerSettings settings;
        private readonly NodeProvider nodeProvider;
        
        public XmlSerializer() : this(new XmlSerializerSettingsBuilder().GetSettings())
        {
        }

        public XmlSerializer(XmlSerializerSettings settings)
        {
            this.settings = settings;
            Preconditions.NotNull(settings, "settings");

            nodeProvider = new NodeProvider(settings);
        }

        public void Serialize(object obj, XmlWriter xmlWriter)
        {
            Preconditions.NotNull(obj, "obj");
            Preconditions.NotNull(xmlWriter, "xmlWriter");

            var visitor = new SerializationVisitor(xmlWriter, nodeProvider);
            visitor.Visit(obj);
        }

        public object Deserialize(Type type, XmlReader xmlReader)
        {
            Preconditions.NotNull(type, "type");
            Preconditions.NotNull(xmlReader, "xmlReader");

            var visitor = new DeserializationVisitor(xmlReader, settings, nodeProvider);
            return visitor.Visit(type);
        }
    }
}