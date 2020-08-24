using System.Threading.Tasks;
using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<ServiceResponse<string>> GetUserNameAsync(string userId);
        Task<ServiceResponse<ApplicationUser>> GetUserAsync(string userId);
        Task<ServiceResponse<string>> CreateUserAsync(string userName, string password);
        Task<ServiceResponse<bool>> DeleteUserAsync(string userId);
        Task<ServiceResponse<ApplicationUser>> ValidateLoginAsync(string userName, string password);
    }
}
