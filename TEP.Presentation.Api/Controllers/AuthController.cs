using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Auth.Commands;
using TEP.Presentation.Api.Controllers.Authorizers;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers
{    

    [Produces("application/json")]
    [Route("api/login")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody]AuthenticateCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return new OkObjectResult(token);
            }
            catch (Exception e)
            {

                return new UnauthorizedObjectResult(e.Message);
            }
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
