using Microsoft.AspNetCore.Authorization;
using System.Linq;
using TEP.Shared;

namespace TEP.Servicos.Api.Controllers.Authorizers
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        /// <summary>
        /// Takes enums into a string to be used in authorize.
        /// </summary>
        /// <param name="allowedRoles">All roles that can access this endpoint.</param>
        public AuthorizeRoles(params UserRoles[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => UserRoles.GetName(typeof(UserRoles), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}