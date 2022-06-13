using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubController: ControllerBase
    {
        public IList<FootballClub> clubs = new List<FootballClub>
        {
            new FootballClub
            {
                Competitions = null,
                Country = "Serbia",
                Id = 1,
                Name = "Crvena Zvezda"
            },
            new FootballClub
            {
                Competitions = null,
                Country = "Serbia",
                Id = 2,
                Name = "Partizan"
            }
        };

        // If we don't use swagger, the HttpGet attribute isn't necessary. The Ina ApiController Get prefix stands for HTTP Get controller.
        [HttpGet("GetAll")]
        public ActionResult<IList<FootballClub>> Get()
        {
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public ActionResult<FootballClub> GetById(int id)
        {
            return Ok(clubs.FirstOrDefault(x=>x.Id == id));
        }
    }
}