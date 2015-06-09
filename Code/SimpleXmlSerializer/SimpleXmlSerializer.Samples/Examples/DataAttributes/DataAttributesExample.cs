using System;

namespace SimpleXmlSerializer.Samples.Examples.DataAttributes
{
    public static class DataAttributesExample
    {
        public static void Execute()
        {
            var serializerSettings = new XmlSerializerSettingsBuilder().UseDataAttributes().GetSettings();
            var serializer = new XmlSerializer(serializerSettings);

            var xml = serializer.SerializeToString(new Company());

            Console.WriteLine(xml);
        }
    }
}