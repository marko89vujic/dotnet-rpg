using System.Collections.Generic;

namespace dotnet_rpg.Models
{
    public class FootballClub
    {
        public int Id { get; set; }

        public string Name { get; set;}

        public string Country {get; set; }

        public string Stadium { get; set; }

        public ICollection<Competition> Competitions {get; set;}

        public User User { get; set; }
    }
}