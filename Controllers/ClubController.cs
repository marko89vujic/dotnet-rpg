using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubController: ControllerBase
    {
        public FootballClub club = new FootballClub
        {
            Competitions = null,
            Country = "Serbia",
            Id = 1,
            Name = "Crvena Zvezda"
        };

        // If we don't use swagger, the HttpGet attribute isn't necessary. The Ina ApiController Get prefix stands for HTTP Get controller.
        [HttpGet]
        public ActionResult<FootballClub> Get()
        {
            return Ok(club);
        }
    }
}