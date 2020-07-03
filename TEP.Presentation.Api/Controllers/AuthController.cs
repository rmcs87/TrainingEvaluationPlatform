//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using TEP.Presentation.Api.Controllers.Authorizers;
//using TEP.Presentation.AuthProvider.Models;
//using TEP.Presentation.AuthProvider.Repositories;
//using TEP.Presentation.AuthProvider.Services;
//using TEP.Shared;

//namespace TEP.Presentation.Api.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/login")]
//    [Authorize]
//    public class AuthController : Controller
//    {
//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<ActionResult> Authenticate([FromBody]User data)
//        {
//            var user = UserRepository.Get(data.Username, data.Password);

//            if (user == null)
//                return NotFound(new { message = "Usuário ou senha inválidos" });

//            var token = TokenService.GenerateToken(user);
//            user.Password = "";

//            return new OkObjectResult(new { user, token });
//        }

//        [HttpGet]
//        [Route("auth_test")]
//        [AuthorizePolicy(UserPolicies.ManagerRights)]
//        public async Task<IActionResult> TestPermissions()
//        {
//            return new OkResult();
//        }

//    }
//}
