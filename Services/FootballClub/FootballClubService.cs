using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.FootballClub;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.FootballClub
{
    public class FootballClubService : IFootballClubService
    {
        private IMapper _mapper;
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

        public FootballClubService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task AddFootballClub(AddFootballClubDto footballClubDto)
        {
            var newClub = _mapper.Map<Models.FootballClub>(footballClubDto);
            newClub.Id = _clubs.Max(x => x.Id) + 1;
            _clubs.Add(newClub);
            return Task.CompletedTask;
        }

        public async Task<ServiceResponse<GetFootballClubDto>> UpdateFootballClub(UpdatedFootballClubDto updatedFootballClub)
        {
            var serviceResponse = new ServiceResponse<GetFootballClubDto>();
            var club = _clubs.FirstOrDefault(x => x.Id == updatedFootballClub.Id);

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
            var footballClub = _clubs.FirstOrDefault(x => x.Id == id);
            var serviceResponse = new ServiceResponse<List<GetFootballClubDto>>();

            if (footballClub != null)
            {
                _clubs.Remove(footballClub);

                serviceResponse.Data = _mapper.Map<List<GetFootballClubDto>>(_clubs.ToList());

                return serviceResponse;
            }

            serviceResponse.Message = $"There is no item with id {id.ToString()}";
            serviceResponse.Success = false;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFootballClubDto>>> GetAllFootballClubs()
        {
            var serviceResponse = new ServiceResponse<List<GetFootballClubDto>>();
            serviceResponse.Data = _mapper.Map<List<GetFootballClubDto>>(_clubs.ToList());
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFootballClubDto>> GetFootballClubById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFootballClubDto>();
            serviceResponse.Data = _mapper.Map<GetFootballClubDto>(_clubs.FirstOrDefault(x => x.Id == id));
            return serviceResponse;
        }
    }
}
