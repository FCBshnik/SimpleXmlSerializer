using System.Runtime.Serialization;

namespace SimpleXmlSerializer.Samples.Examples.DataAttributes
{
    [DataContract(Name = "Worker")]
    public class Employee
    {
        public Employee()
        {
            FirstName = "John";
            Age = 45;
        }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "Age")]
        public int Age { get; set; }
    }
}