using System;
using System.IO;
using System.Text;
using System.Xml;
using SimpleXmlSerializer.Core;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Extensions
{
    public static class XmlSerializerExtensions
    {
         public static string Serialize(this XmlSerializer xmlSerializer, object obj)
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

         public static object Deserialize(this XmlSerializer xmlSerializer, Type type, string xml)
         {
             Preconditions.NotNull(xmlSerializer, "xmlSerializer");
             Preconditions.NotNull(xml, "xml");

             using (var stringReader = new StringReader(xml))
             {
                 using (var xmlReader = XmlReader.Create(stringReader))
                 {
                     return xmlSerializer.Deserialize(type, xmlReader);
                 }
             }
         }
    }
}