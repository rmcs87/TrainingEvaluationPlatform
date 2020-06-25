using System;
using System.Collections.Generic;
using System.Linq;
using TEP.Services.AuthProvider.Models;

namespace TEP.Services.AuthProvider.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "rico", Password = "r1c0", Role = "admin" },
                new User { Id = 2, Username = "tom", Password = "mot", Role = "manager" }
            };
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password.ToLower() ).FirstOrDefault();
        }
    }
}
