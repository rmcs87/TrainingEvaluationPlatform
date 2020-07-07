using Microsoft.AspNetCore.Authorization;
using System.Linq;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers.Authorizers
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params UserRoles[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => UserRoles.GetName(typeof(UserRoles), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}