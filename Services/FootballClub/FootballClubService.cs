using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.FootballClub
{
    public class FootballClubService : IFootballClubService
    {
        private IList<Models.FootballClub> _clubs = new List<Models.FootballClub>
        {
            new Models.FootballClub
            {
                Competitions = null,
                Country = "Serbia",
                Id = 1,
                Stadium = "Rajko Mitic",
                Name = "Crvena Zvezda"
            },
            new Models.FootballClub
            {
                Competitions = null,
                Country = "Serbia",
                Id = 2,
                Stadium = "JNA",
                Name = "Partizan"
            }
        };

        public void AddFootballClub(Models.FootballClub footballClub)
        {
            _clubs.Add(footballClub);
        }

        public List<Models.FootballClub> GetAllFootballClubs()
        {
            return _clubs.ToList();
        }

        public Models.FootballClub GetFootballClubById(int id)
        {
            return _clubs.FirstOrDefault(x => x.Id == id);
        }
    }
}
