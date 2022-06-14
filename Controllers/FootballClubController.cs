using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;
using dotnet_rpg.Services.FootballClub;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IList<FootballClub>> Get()
        {
            return Ok(_footballClubService.GetAllFootballClubs());
        }

        [HttpGet("{id}")]
        public ActionResult<FootballClub> GetById(int id)
        {
            return Ok(_footballClubService.GetFootballClubById(id));
        }

        [HttpPost]
        public ActionResult<List<FootballClub>> AddNewClub(FootballClub footballClub)
        {
            _footballClubService.AddFootballClub(footballClub);

            return _footballClubService.GetAllFootballClubs();
        }
    }
}