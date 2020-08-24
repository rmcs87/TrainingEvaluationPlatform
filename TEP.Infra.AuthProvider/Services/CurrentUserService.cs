using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;

namespace TEP.Infra.AuthProvider
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            RecoverUserId = new ServiceResponse<string>();
            RecoverUserId.Data = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(RecoverUserId.Data == null)
            {
                RecoverUserId.Data = "";
                RecoverUserId.Success = false;
                RecoverUserId.Message = "Current Claim NameIdentifier not Found.";
            }
        }

        public ServiceResponse<string> RecoverUserId { get; }
    }
}
