using System;
using System.IO;
using System.Text;
using System.Xml;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer
{
    public static class XmlSerializerExtensions
    {
        public static string SerializeToString(this XmlSerializer xmlSerializer, object obj)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);
            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(obj, xmlWriter);
            }

            return stringWriter.ToString();
        }

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

        public static object DeserializeFromStream(this XmlSerializer xmlSerializer, Type type, Stream inputStream)
        {
            if (xmlSerializer == null)
                throw new ArgumentNullException("xmlSerializer");

            using (var xmlReader = XmlReader.Create(inputStream))
            {
                return xmlSerializer.Deserialize(type, xmlReader);
            }
        }

        public static void SerializeToFile(this XmlSerializer xmlSerializer, object obj, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.SerializeToStream(obj, fileStream);
            }
        }

        public static object DeserializeFromFile(this XmlSerializer xmlSerializer, Type type, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return xmlSerializer.DeserializeFromStream(type, fileStream);
            }
        }
    }
}