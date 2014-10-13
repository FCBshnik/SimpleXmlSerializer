using System;
using System.IO;
using System.Text;
using System.Xml;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Extensions
{
    public static class XmlSerializerExtensions
    {
         public static string SerializeToString(this XmlSerializer xmlSerializer, object obj)
         {
             Preconditions.NotNull(xmlSerializer, "xmlSerializer");

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
             Preconditions.NotNull(xmlSerializer, "xmlSerializer");
             Preconditions.NotNull(type, "type");
             Preconditions.NotNull(xml, "xml");

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
             Preconditions.NotNull(xmlSerializer, "xmlSerializer");
             
             var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

             using (var xmlWriter = XmlWriter.Create(outputStream, settings))
             {
                 xmlSerializer.Serialize(obj, xmlWriter);
             }
         }

         public static object DeserializeFromStream(this XmlSerializer xmlSerializer, Type type, Stream inputStream)
         {
             Preconditions.NotNull(xmlSerializer, "xmlSerializer");
             Preconditions.NotNull(type, "type");

             using (var xmlReader = XmlReader.Create(inputStream))
             {
                 return xmlSerializer.Deserialize(type, xmlReader);
             }
         }
    }
}