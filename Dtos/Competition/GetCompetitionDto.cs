using System.Collections.Generic;

namespace dotnet_rpg.Dtos.Competition
{
    public class GetCompetitionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Prize { get; set; }

        public ICollection<Models.FootballClub> Clubs { get; set; }
    }
}
