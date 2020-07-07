using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.AuthProvider
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
