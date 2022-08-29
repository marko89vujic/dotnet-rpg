using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Competition;
using dotnet_rpg.Models;
using dotnet_rpg.Services.Competition;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionController: ControllerBase
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IList<GetCompetitionDto>>>> Get()
        {
            return Ok(await _competitionService.GetAllCompetitions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCompetitionDto>>> GetById(int id)
        {
            return Ok(await _competitionService.GetCompetitionById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCompetitionDto>>>> AddNewCompetition(
            AddCompetitionDto addCompetitionDto)
        {
            await _competitionService.AddCompetition(addCompetitionDto);

            return await _competitionService.GetAllCompetitions();
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCompetitionDto>>> Update(
            UpdateCompetitionDto updateCompetitionDto)
        {
            var serviceResponse = await _competitionService.UpdateCompetition(updateCompetitionDto);

            if (serviceResponse.Data != null)
            {
                return Ok(serviceResponse);
            }

            return NotFound(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IList<GetCompetitionDto>>>> DeleteCompetitionById(int id)
        {
            var serviceResponse = await _competitionService.DeleteCompetitionById(id);

            if (serviceResponse.Data != null)
            {
                return Ok(serviceResponse);
            }

            return NotFound(serviceResponse);
        }
    }
}