using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimpleXmlSerializer.Samples.Examples.DataAttributes
{
    [DataContract(Name = "Organization")]
    public class Company
    {
        public Company()
        {
            Name = "Some company";
            Employees = new[] { new Employee() };
        }

        [DataMember(Name = "FullName")]
        public string Name { get; set; }

        [DataMember(Name = "Workers")]
        public IEnumerable<Employee> Employees { get; set; }
    }
}