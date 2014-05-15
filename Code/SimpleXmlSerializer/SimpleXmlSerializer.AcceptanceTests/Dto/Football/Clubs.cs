using System;

namespace SimpleXmlSerializer.AcceptanceTests.Dto.Football
{
    public class Clubs
    {
        public static Club Barca
        {
            get 
            { 
                return new Club
                    {
                        Name = "Barcelona", 
                        Team = Teams.Barca, 
                        Coach = Coaches.Martino,
                        Founded = new DateTime(1899, 11, 29, 0, 0, 0, DateTimeKind.Utc),
                        President = "Josep Maria Bartomeu",
                    }; 
            }
        }
    }
}