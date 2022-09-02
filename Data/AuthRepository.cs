using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private FootballWorldDataContext _footballWorldDataContext;

        private readonly IConfiguration _configuration;

        public AuthRepository(FootballWorldDataContext footballWorldDataContext, IConfiguration configuration)
        {
            _footballWorldDataContext = footballWorldDataContext;
            _configuration = configuration;
        }


        public async Task<bool> IsUserExist(string userName)
        {
            if (await _footballWorldDataContext.Users.AnyAsync(x => x.Username.ToLower() == userName.ToLower()))
            {
                return true;
            }

            return false;
        }

        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _footballWorldDataContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == userName.ToLower());

            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Incorrect password";
            }
            else
            {
                serviceResponse.Data =CreateToken(user);
            }

            return serviceResponse; 

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();

            if (await IsUserExist(user.Username))
            {
                serviceResponse.Message = "User is already exist";
                serviceResponse.Success = false;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _footballWorldDataContext.Users.Add(user);
            await _footballWorldDataContext.SaveChangesAsync();
            
            serviceResponse.Data = user.Id;

            return serviceResponse;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);



            return  tokenHandler.WriteToken(token);
        }
    }
}
