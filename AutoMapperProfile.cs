using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.FootballClub;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FootballClub, GetFootballClubDto>();
            CreateMap<AddFootballClubDto, FootballClub>();
        }
    }
}
