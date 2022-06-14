using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.FootballClub;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.FootballClub
{
    public interface IFootballClubService
    {
        Task<ServiceResponse<List<GetFootballClubDto>>> GetAllFootballClubs();

        Task<ServiceResponse<GetFootballClubDto>> GetFootballClubById(int id);

        Task AddFootballClub(AddFootballClubDto footballClub);

    }
}
