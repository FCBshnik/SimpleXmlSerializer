using System;
using System.Xml;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer
{
    /// <summary>
    /// Responsible to serialize/deserialize objects to/from xml.
    /// </summary>
    public class XmlSerializer
    {
        private readonly NodeProvider nodeProvider;
        
        public XmlSerializer() : this(new XmlSerializerSettingsBuilder().GetSettings())
        {
        }

        public XmlSerializer(XmlSerializerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            nodeProvider = new NodeProvider(settings);
        }

        /// <summary>
        /// Serializes specified object using provided <see cref="XmlWriter"/>.
        /// </summary>
        public void Serialize(object value, XmlWriter xmlWriter)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (xmlWriter == null)
                throw new ArgumentNullException("xmlWriter");

            var visitor = new SerializationVisitor(xmlWriter, nodeProvider);
            visitor.Visit(value);
        }

        /// <summary>
        /// Deserializes object of specified type using provided <see cref="XmlReader"/>.
        /// </summary>
        public object Deserialize(Type type, XmlReader xmlReader)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (xmlReader == null)
                throw new ArgumentNullException("xmlReader");

            var visitor = new DeserializationVisitor(xmlReader, nodeProvider);
            return visitor.Visit(type);
        }
    }
}