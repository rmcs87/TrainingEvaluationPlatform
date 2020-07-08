using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Assets.Commands.DeleteAsset;
using TEP.Application.Assets.Commands.UpdateAsset;
using TEP.Application.Common.Exceptions;
using TEP.Presentation.Api.Controllers.Authorizers;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/asset")]
    [Authorize]
    public class AssetController : TEPControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public async Task<ActionResult> Insert([FromBody] CreateAssetCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return new OkObjectResult(new { id });
            }
            catch (ValidationException ve)
            {
                return new BadRequestObjectResult(new { errorMessage = ve.Message, errorList = ve.Errors });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public async Task<ActionResult> Update([FromBody] UpdateAssetComamnd command)
        {
            try
            {
                await _mediator.Send(command);
                return new OkResult();
            }
            catch (ValidationException ve)
            {
                return new BadRequestObjectResult(new { errorMessage = ve.Message, errorList = ve.Errors });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteAssetCommand { Id = id });
                return new OkResult();
            }
            catch (NotFoundException ve)
            {
                return new BadRequestObjectResult(new { errorMessage = ve.Message });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}

