using System.Collections.Generic;

namespace dotnet_rpg.Dtos.FootballClub
{
    public class UpdatedFootballClubDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Stadium { get; set; }

        public ICollection<Models.Competition> Competitions { get; set; }
    }
}
