using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Assets.Commands.DeleteAsset;
using TEP.Application.Assets.Commands.UpdateAsset;
using TEP.Application.Assets.Queries.GetAsset;
using TEP.Application.Assets.Queries.GetAssetImg;
using TEP.Application.Assets.Queries.ListAssets;
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

        [HttpGet]
        [AuthorizePolicy(UserPolicies.SupervisorRights)]
        public async Task<ActionResult> List()
        {
            try
            {
                var assets = await _mediator.Send(new ListAssetsQuery());
                return new OkObjectResult(new { assets });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AuthorizePolicy(UserPolicies.SupervisorRights)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var asset = await _mediator.Send(new GetAssetQuery { Id = id });
                return new OkObjectResult(asset);
            }
            catch (NotFoundException nf)
            {
                return new NotFoundObjectResult(new { errorMessage = "Asset not found." });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("image/{fileName}")]
        [AuthorizePolicy(UserPolicies.SupervisorRights)]
        public async Task<ActionResult> GetImage(string fileName)
        {
            try
            {
                var file = await _mediator.Send(new GetAssetImgQuery { ImgName = fileName });
                return new PhysicalFileResult(file.FilePath, file.MimeType);
            }
            catch (NotFoundException nf)
            {
                return new NotFoundObjectResult(new { errorMessage = "File not found." });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public async Task<ActionResult> Insert([FromForm] CreateAssetCommand command)
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
        public async Task<ActionResult> Update([FromForm] UpdateAssetComamnd command)
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
            catch (Exception e)
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
            catch (NotFoundException nf)
            {
                return new NotFoundObjectResult(new { errorMessage = nf.Message });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}

