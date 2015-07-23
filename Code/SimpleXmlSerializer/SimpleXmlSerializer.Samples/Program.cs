using System;
using SimpleXmlSerializer.Samples.Examples.DataAttributes;
using SimpleXmlSerializer.Samples.Examples.NoAttributes;
using SimpleXmlSerializer.Samples.Examples.XmlAttributes;

namespace SimpleXmlSerializer.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("No attributes:");
            Console.WriteLine();
            NoAttributesExample.Execute();
            Console.WriteLine();

            Console.WriteLine("Xml attributes:");
            Console.WriteLine();
            XmlAttributesExample.Execute();
            Console.WriteLine();

            Console.WriteLine("Data attributes:");
            Console.WriteLine();
            DataAttributesExample.Execute();
        }
    }
}
