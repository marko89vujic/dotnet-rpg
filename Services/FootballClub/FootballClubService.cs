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

        public async Task AddFootballClub(AddFootballClubDto footballClubDto)
        {
            var newClub = _mapper.Map<Models.FootballClub>(footballClubDto);
            newClub.Id = _clubs.Max(x => x.Id) + 1;
              _clubs.Add(newClub);
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
