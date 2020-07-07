using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
        ApplicationUser ValidateLogin(string userName, string password);
    }
}
