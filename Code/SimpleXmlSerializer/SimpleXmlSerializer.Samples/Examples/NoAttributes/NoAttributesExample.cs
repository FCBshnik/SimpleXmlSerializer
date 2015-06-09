using System;

namespace SimpleXmlSerializer.Samples.Examples.NoAttributes
{
    public static class NoAttributesExample
    {
        public static void Execute()
        {
            var serializer = new XmlSerializer();

            var xml = serializer.SerializeToString(new Company());

            Console.WriteLine(xml);
        }
    }
}