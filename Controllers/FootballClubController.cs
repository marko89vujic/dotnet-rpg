using dotnet_rpg.Models;
using dotnet_rpg.Services.FootballClub;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.FootballClub;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballClubController: ControllerBase
    {
        private readonly IFootballClubService _footballClubService;

        public FootballClubController(IFootballClubService footballClubService)
        {
            _footballClubService = footballClubService;
        }

        // If we don't use swagger, the HttpGet attribute isn't necessary. The Ina ApiController Get prefix stands for HTTP Get controller.
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IList<GetFootballClubDto>>>> Get()
        {
            return Ok(await _footballClubService.GetAllFootballClubs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetFootballClubDto>>> GetById(int id)
        {
            return Ok(await _footballClubService.GetFootballClubById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetFootballClubDto>>>> AddNewClub(AddFootballClubDto footballClubDto)
        {
            await _footballClubService.AddFootballClub(footballClubDto);

            return await _footballClubService.GetAllFootballClubs();
        }
    }
}