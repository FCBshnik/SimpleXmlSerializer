namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Coaches
    {
        public static Coach Unknown
        {
            get { return new Coach { Name = "Unknown", Age = 999 }; }
        }

        public static Coach Martino
        {
            get { return new Coach { Name = "Martino", Age = 51 }; }
        }
    }
}