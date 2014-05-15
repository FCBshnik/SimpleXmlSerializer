namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Players
    {
        public static Player Xavi
        {
            get { return new Player { Name = "Xavi", Number = 6 }; }
        }

        public static Player Iniesta
        {
            get { return new Player { Name = "Iniesta", Number = 8 }; }
        }
    }
}