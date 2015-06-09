using SimpleXmlSerializer.Samples.Examples.DataAttributes;
using SimpleXmlSerializer.Samples.Examples.NoAttributes;
using SimpleXmlSerializer.Samples.Examples.XmlAttributes;

namespace SimpleXmlSerializer.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NoAttributesExample.Execute();

            XmlAttributesExample.Execute();

            DataAttributesExample.Execute();
        }
    }
}
