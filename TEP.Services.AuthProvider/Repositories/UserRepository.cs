using System.Collections.Generic;
using System.Linq;
using TEP.Services.AuthProvider.Models;
using TEP.Shared;

namespace TEP.Services.AuthProvider.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "rico", Password = "r1c0", Role = UserRoles.Manager },
                new User { Id = 2, Username = "tom", Password = "mot", Role = UserRoles.Admin },
                new User { Id = 3, Username = "joao", Password = "jonh", Role = UserRoles.Operator }
            };
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password.ToLower() ).FirstOrDefault();
        }
    }
}
