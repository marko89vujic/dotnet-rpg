using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private FootballWorldDataContext _footballWorldDataContext;

        public AuthRepository(FootballWorldDataContext footballWorldDataContext)
        {
            _footballWorldDataContext = footballWorldDataContext;
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
                serviceResponse.Data = user.Id.ToString();
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
    }
}
