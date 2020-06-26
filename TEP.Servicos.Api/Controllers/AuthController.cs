using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TEP.Services.AuthProvider.Models;
using TEP.Services.AuthProvider.Repositories;
using TEP.Services.AuthProvider.Services;
using TEP.Servicos.Api.Controllers.Authorizers;
using TEP.Shared;

namespace TEP.Servicos.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    [Authorize]
    public class AuthController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody]User data)
        {
            var user = UserRepository.Get(data.Username, data.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new OkObjectResult(new { user, token });
        }

        [HttpGet]
        [Route("auth_test")]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public async Task<IActionResult> TestPermissions()
        {
            return new OkResult();
        }

    }
}
