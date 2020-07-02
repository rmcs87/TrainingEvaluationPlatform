using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace TEP.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId);
        SecurityKey Loadkey();
    }
}
