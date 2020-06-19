using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Servicos.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ControllerBase<Entity, EntityDTO> : Controller
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
        public virtual async Task<IActionResult> Insert([FromBody] EntityDTO data)
        {

            if (!ModelState.IsValid)
            {
                string json = GetModelStateErrosAsJson();
                return BadRequest(json);
            }

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
        public IActionResult Update([FromBody] EntityDTO data)
        {
            if (!ModelState.IsValid)
            {
                string json = GetModelStateErrosAsJson();
                return BadRequest(json);
            }

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
        public IActionResult Delete(int id)
        {
            try
            {
                _app.Delete(id);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected string GetModelStateErrosAsJson()
        {
            return JsonSerializer.Serialize(
                new
                {
                    Errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage)
                    )
                }
            );
        }
    }
}
