using System.Collections.Generic;

namespace dotnet_rpg.Services.FootballClub
{
    public interface IFootballClubService
    {
        List<Models.FootballClub> GetAllFootballClubs();

        Models.FootballClub GetFootballClubById(int id);

        void AddFootballClub(Models.FootballClub footballClub);

    }
}
