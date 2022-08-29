using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Competition;
using dotnet_rpg.Dtos.FootballClub;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.Competition
{
    public interface ICompetitionService
    {
        Task<ServiceResponse<List<GetCompetitionDto>>> GetAllCompetitions();

        Task<ServiceResponse<GetCompetitionDto>> GetCompetitionById(int id);

        Task AddCompetition(AddCompetitionDto competition);

        Task<ServiceResponse<GetCompetitionDto>> UpdateCompetition(UpdateCompetitionDto updateCompetitionDto);

        Task<ServiceResponse<List<GetCompetitionDto>>> DeleteCompetitionById(int id);
    }
}
