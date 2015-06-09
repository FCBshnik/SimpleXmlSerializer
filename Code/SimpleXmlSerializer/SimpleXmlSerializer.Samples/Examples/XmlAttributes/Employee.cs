using System.Xml.Serialization;

namespace SimpleXmlSerializer.Samples.Examples.XmlAttributes
{
    public class Employee
    {
        public Employee()
        {
            FirstName = "John";
            Age = 45;
        }

        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlAttribute(AttributeName = "age")]
        public int Age { get; set; }
    }
}