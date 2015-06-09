namespace SimpleXmlSerializer.Samples.Examples.NoAttributes
{
    public class Employee
    {
        public Employee()
        {
            FirstName = "John";
            Age = 45;
        }

        public string FirstName { get; set; }

        public int Age { get; set; }
    }
}