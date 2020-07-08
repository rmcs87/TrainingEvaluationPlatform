using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TEP.Application.Auth.Commands;
using TEP.Application.Common.Exceptions;
using TEP.Infra.AuthProvider.Exceptions;
using TEP.Presentation.Api.Controllers.Authorizers;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    [Authorize]
    public class AuthController : TEPControllerBase
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
                return new OkObjectResult(new { accessToken = token });
            }
            catch (InvalidUserException iue)
            {
                return new UnauthorizedObjectResult(new { errorMessage = iue.Message });
            }
            catch(ValidationException ve)
            {
                return new BadRequestObjectResult(new { errorMessage = ve.Message, errorList = ve.Errors });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
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
