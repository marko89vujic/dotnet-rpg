using System.Collections.Generic;

namespace dotnet_rpg.Models
{
    public class Competition
    {
        public int Id {get; set;}

        public string Name {get; set;}

        public int Prize {get; set;}

        public ICollection<FootballClub> Clubs {get; set;} 
    }
}