namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Teams
    {
        public static Team Barca
        {
            get { return new Team { Midfielders = new []{ Players.Xavi, Players.Iniesta } }; }
        }
    }
}