using System;
using System.IO;
using System.Text;
using System.Xml;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer
{
    public static class XmlSerializerExtensions
    {
        /// <summary>
        /// Serializes specified object to string.
        /// </summary>
        public static string SerializeToString(this XmlSerializer xmlSerializer, object value)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);
            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(value, xmlWriter);
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Deserializes object of specified type from string.
        /// </summary>
        public static object DeserializeFromString(this XmlSerializer xmlSerializer, Type type, string xml)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            using (var stringReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    return xmlSerializer.Deserialize(type, xmlReader);
                }
            }
        }

        /// <summary>
        /// Deserializes object of specified type from string.
        /// </summary>
        public static TValue DeserializeFromString<TValue>(this XmlSerializer xmlSerializer, string xml)
        {
            return (TValue) DeserializeFromString(xmlSerializer, typeof(TValue), xml);
        }

        /// <summary>
        /// Serializes specified object to stream.
        /// </summary>
        public static void SerializeToStream(this XmlSerializer xmlSerializer, object obj, Stream outputStream)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            using (var xmlWriter = XmlWriter.Create(outputStream, settings))
            {
                xmlSerializer.Serialize(obj, xmlWriter);
            }
        }

        /// <summary>
        /// Deserializes object of specified type from stream.
        /// </summary>
        public static object DeserializeFromStream(this XmlSerializer xmlSerializer, Type type, Stream inputStream)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            using (var xmlReader = XmlReader.Create(inputStream))
            {
                return xmlSerializer.Deserialize(type, xmlReader);
            }
        }

        /// <summary>
        /// Deserializes object of specified type from stream.
        /// </summary>
        public static TValue DeserializeFromStream<TValue>(this XmlSerializer xmlSerializer,  Stream inputStream)
        {
            return (TValue) DeserializeFromStream(xmlSerializer, typeof(TValue), inputStream);
        }

        /// <summary>
        /// Serializes specified object to file.
        /// </summary>
        public static void SerializeToFile(this XmlSerializer xmlSerializer, object obj, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.SerializeToStream(obj, fileStream);
            }
        }

        /// <summary>
        /// Deserializes object of specified type from file.
        /// </summary>
        public static object DeserializeFromFile(this XmlSerializer xmlSerializer, Type type, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return xmlSerializer.DeserializeFromStream(type, fileStream);
            }
        }

        /// <summary>
        /// Deserializes object of specified type from file.
        /// </summary>
        public static TValue DeserializeFromFile<TValue>(this XmlSerializer xmlSerializer, string filePath)
        {
            return (TValue) DeserializeFromFile(xmlSerializer, typeof(TValue), filePath);
        }
    }
}