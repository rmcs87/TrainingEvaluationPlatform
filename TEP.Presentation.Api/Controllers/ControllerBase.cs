using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Presentation.Api.Controllers.Authorizers;
using TEP.Shared;

namespace TEP.Presentation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class ControllerBase<Entity, EntityDTO> : Controller
       where Entity : EntityBase
       where EntityDTO : DTOBase
    {
        readonly protected IAppBase<Entity, EntityDTO> _app;

        public ControllerBase(IAppBase<Entity, EntityDTO> app)
        {
            this._app = app;
        }

        [HttpGet]
        [Route("")]
        [AuthorizePolicy(UserPolicies.SupervisorRights)]
        public IActionResult List()
        {
            try
            {
                var result = _app.List();

                if (result == null)
                    return NotFound($"{nameof(Entity)} Not Found ");

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AuthorizePolicy(UserPolicies.SupervisorRights)]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _app.GetById(id);
                
                if (result == null)
                    return NotFound($"{typeof(Entity).Name} with ID = {id} Not Found ");

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public virtual async Task<IActionResult> Insert([FromBody] EntityDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return new OkObjectResult(new { id =_app.Insert(data) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public virtual async Task<IActionResult> Update([FromBody] EntityDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _app.Update(data);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                _app.Delete(id);
                return new OkObjectResult(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
