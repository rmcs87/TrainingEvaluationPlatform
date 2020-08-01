using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
using TEP.Infra.AuthProvider.Exceptions;
using TEP.Shared;

namespace TEP.Infra.AuthProvider
{
    public class IdentityService : IIdentityService
    {
        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var id = Int32.Parse(userId);

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = 1, Username = "rico", Password = "r1c0", Role = UserRoles.Manager },
                new ApplicationUser { Id = 2, Username = "tom", Password = "mot", Role = UserRoles.Admin },
                new ApplicationUser { Id = 3, Username = "joao", Password = "jonh", Role = UserRoles.Operator }
            };

            return users.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var id = Int32.Parse(userId);

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = 1, Username = "rico", Password = "r1c0", Role = UserRoles.Manager },
                new ApplicationUser { Id = 2, Username = "tom", Password = "mot", Role = UserRoles.Admin },
                new ApplicationUser { Id = 3, Username = "joao", Password = "jonh", Role = UserRoles.Operator }
            };

            return users.Where(x => x.Id == id).FirstOrDefault().Username;
        }

        public ApplicationUser ValidateLogin(string userName, string password)
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = 1, Username = "rico", Password = "r1c0", Role = UserRoles.Manager },
                new ApplicationUser { Id = 2, Username = "tom", Password = "mot", Role = UserRoles.Admin },
                new ApplicationUser { Id = 3, Username = "joao", Password = "jonh", Role = UserRoles.Operator }
            };

            if (!users.Where(x => x.Username == userName && x.Password == password).Any())
            {
                throw new InvalidUserException("Invalid Username and/or password.");
            }

            return users.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
        }
    }
}
