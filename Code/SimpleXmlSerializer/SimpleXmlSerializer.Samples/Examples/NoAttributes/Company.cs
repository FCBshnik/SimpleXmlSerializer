using System.Collections.Generic;

namespace SimpleXmlSerializer.Samples.Examples.NoAttributes
{
    public class Company
    {
        public Company()
        {
            Name = "Some company";
            Employees = new[] { new Employee() };
        }

        public string Name { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}