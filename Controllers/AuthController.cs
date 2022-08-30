using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegistrationDto userRegistrationDto)
        {
            var response = await _authRepository.Register(new User { Username = userRegistrationDto.Username },
                userRegistrationDto.Password);

            if (response.Success == true)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto userLoginDto)
        {
            var response = await _authRepository.Login(userLoginDto.Username,userLoginDto.Password);

            if (response.Success == true)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
