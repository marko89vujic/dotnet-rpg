using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Dtos.FootballClub
{
    public class GetFootballClubDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Stadium { get; set; }

        public ICollection<Competition> Competitions { get; set; }
    }
}
