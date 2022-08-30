using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private FootballWorldDataContext _footballWorldDataContext;

        public AuthRepository(FootballWorldDataContext footballWorldDataContext)
        {
            _footballWorldDataContext = footballWorldDataContext;
        }


        public Task<bool> IsUserExist(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _footballWorldDataContext.Users.Add(user);
            await _footballWorldDataContext.SaveChangesAsync();

            var serviceResponse = new ServiceResponse<int>();

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
    }
}
