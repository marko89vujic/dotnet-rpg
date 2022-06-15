using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.FootballClub;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FootballClub
{
    public class FootballClubService : IFootballClubService
    {
        private IMapper _mapper;
        private readonly FootballWorldDataContext _footballWorldDataContext;

        public FootballClubService(IMapper mapper, FootballWorldDataContext footballWorldDataContext)
        {
            _mapper = mapper;
            _footballWorldDataContext = footballWorldDataContext;
        }

        public async Task AddFootballClub(AddFootballClubDto footballClubDto)
        {
            var newClub = _mapper.Map<Models.FootballClub>(footballClubDto);
            _footballWorldDataContext.FootballClubs.Add(newClub);
            await _footballWorldDataContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<GetFootballClubDto>> UpdateFootballClub(UpdatedFootballClubDto updatedFootballClub)
        {
            var serviceResponse = new ServiceResponse<GetFootballClubDto>();
            var club = _footballWorldDataContext.FootballClubs.FirstOrDefault(x => x.Id == updatedFootballClub.Id);

            if (club != null)
            {
                club.Competitions = updatedFootballClub.Competitions;
                club.Country = updatedFootballClub.Country;
                club.Name = updatedFootballClub.Name;
                club.Stadium = updatedFootballClub.Stadium;

                serviceResponse.Data = _mapper.Map<GetFootballClubDto>(club);

                return serviceResponse;
            }

            serviceResponse.Message = "Not updated";
            serviceResponse.Success = false;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFootballClubDto>>> DeleteFootballClubById(int id)
        {
            var footballClub = _footballWorldDataContext.FootballClubs.FirstOrDefault(x => x.Id == id);
            var serviceResponse = new ServiceResponse<List<GetFootballClubDto>>();

            if (footballClub != null)
            {
                _footballWorldDataContext.FootballClubs.Remove(footballClub);

                serviceResponse.Data = _mapper.Map<List<GetFootballClubDto>>(_footballWorldDataContext.FootballClubs.ToList());

                return serviceResponse;
            }

            serviceResponse.Message = $"There is no item with id {id.ToString()}";
            serviceResponse.Success = false;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFootballClubDto>>> GetAllFootballClubs()
        {
            var serviceResponse = new ServiceResponse<List<GetFootballClubDto>>();
            var footballClubs = await _footballWorldDataContext.FootballClubs.AsQueryable().ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetFootballClubDto>>(footballClubs);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFootballClubDto>> GetFootballClubById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFootballClubDto>();
            var footballClub = await _footballWorldDataContext.FootballClubs.AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (footballClub != null)
            {
                serviceResponse.Data = _mapper.Map<GetFootballClubDto>(footballClub);

                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = $"Entity with {id} is not found";
            return serviceResponse;
        }
    }
}
