using Microsoft.AspNetCore.Authorization;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers.Authorizers
{
    public class AuthorizePolicy : AuthorizeAttribute
    {
        public AuthorizePolicy(UserPolicies allowedPolicy)
        {
            Policy = allowedPolicy.ToString();
        }
    }
}
