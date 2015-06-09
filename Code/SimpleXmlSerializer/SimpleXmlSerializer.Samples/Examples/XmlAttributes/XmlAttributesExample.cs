using System;

namespace SimpleXmlSerializer.Samples.Examples.XmlAttributes
{
    public static class XmlAttributesExample
    {
        public static void Execute()
        {
            var serializerSettings = new XmlSerializerSettingsBuilder().UseXmlAttributes().GetSettings();
            var serializer = new XmlSerializer(serializerSettings);

            var xml = serializer.SerializeToString(new Company());

            Console.WriteLine(xml);
        }
    }
}