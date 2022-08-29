using dotnet_rpg.Dtos.Competition;
using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.Competition
{
    public class CompetitionService : ICompetitionService
    {
        private IMapper _mapper;
        private readonly FootballWorldDataContext _footballWorldDataContext;

        public CompetitionService(IMapper mapper, FootballWorldDataContext footballWorldDataContext)
        {
            _mapper = mapper;
            _footballWorldDataContext = footballWorldDataContext;
        }

        public async Task AddCompetition(AddCompetitionDto competitionDto)
        {
            var newCompetation = _mapper.Map<Models.Competition>(competitionDto);
            await _footballWorldDataContext.Competitions.AddAsync(newCompetation);
            await _footballWorldDataContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<List<GetCompetitionDto>>> DeleteCompetitionById(int id)
        {
            var competation = await _footballWorldDataContext.Competitions.FirstOrDefaultAsync(x => x.Id == id);
            var serviceResponse = new ServiceResponse<List<GetCompetitionDto>>();

            if (competation != null)
            {
                _footballWorldDataContext.Competitions.Remove(competation);

                await _footballWorldDataContext.SaveChangesAsync();

                serviceResponse.Data =
                    _mapper.Map<List<GetCompetitionDto>>(_footballWorldDataContext.Competitions.ToList());

                return serviceResponse;
            }

            serviceResponse.Message = $"There is no competition with id {id}";
            serviceResponse.Success = false;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCompetitionDto>>> GetAllCompetitions()
        {
            var serviceResponse = new ServiceResponse<List<GetCompetitionDto>>();
            var competitions = await _footballWorldDataContext.Competitions.AsQueryable().ToListAsync();

            serviceResponse.Data = _mapper.Map<List<GetCompetitionDto>>(competitions);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCompetitionDto>> GetCompetitionById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCompetitionDto>();

            var competition = await _footballWorldDataContext.Competitions.FirstOrDefaultAsync(x => x.Id == id);

            if (competition != null)
            {
                serviceResponse.Data = _mapper.Map<GetCompetitionDto>(competition);

                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = $"There is no competition with {id}";

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCompetitionDto>> UpdateCompetition(UpdateCompetitionDto updateCompetitionDto)
        {
            var competition =
                await _footballWorldDataContext.Competitions.FirstOrDefaultAsync(x => x.Id == updateCompetitionDto.Id);
            var serviceResponse = new ServiceResponse<GetCompetitionDto>();

            if (competition != null)
            {
                competition.Clubs = updateCompetitionDto.Clubs;
                competition.Name = updateCompetitionDto.Name;
                competition.Prize = updateCompetitionDto.Prize;

                serviceResponse.Data = _mapper.Map<GetCompetitionDto>(competition);

                await _footballWorldDataContext.SaveChangesAsync();

                return serviceResponse;
            }

            serviceResponse.Message = $"There is no competition with id {updateCompetitionDto.Id}";
            serviceResponse.Success = false;

            return serviceResponse;
        }
    }
}
