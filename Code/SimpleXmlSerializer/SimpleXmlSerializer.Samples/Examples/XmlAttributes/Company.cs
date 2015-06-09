using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleXmlSerializer.Samples.Examples.XmlAttributes
{
    [XmlRoot(ElementName = "organization")]
    public class Company
    {
        public Company()
        {
            Name = "Some company";
            Employees = new[] { new Employee() };
        }

        [XmlElement(ElementName = "fullName")]
        public string Name { get; set; }

        [XmlArray("workers")]
        [XmlArrayItem("worker")]
        public IEnumerable<Employee> Employees { get; set; }
    }
}