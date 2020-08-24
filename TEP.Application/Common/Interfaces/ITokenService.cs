using System.Threading.Tasks;
using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<ServiceResponse<string>> GenerateTokenAsync(string userId);
    }
}
