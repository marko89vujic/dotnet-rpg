using System;
using dotnet_rpg.Models;
using dotnet_rpg.Services.FootballClub;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.FootballClub;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_rpg.Controllers
{
    [Authorize]
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
            // get user id through the User property inherited from ControllerBase class.
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
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

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFootballClubDto>>> Update(
            UpdatedFootballClubDto updatedFootballClub)
        {
            var serviceResponse =  await _footballClubService.UpdateFootballClub(updatedFootballClub);

            if (serviceResponse.Data != null)
            {
                return Ok(serviceResponse);
            }

            return NotFound(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IList<GetFootballClubDto>>>> DeleteFootballClubById(int id)
        {
            var serviceResponse = await _footballClubService.DeleteFootballClubById(id);

            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}