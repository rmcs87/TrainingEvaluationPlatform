using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Appication.Validators;
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
                    return NotFound($"{typeof(Entity).Name} {id} Not Found ");

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] EntityDTO data)
        {
            /*BaseDTOValidator<EntityDTO> validator = new BaseDTOValidator<EntityDTO>();
            ValidationResult results = validator.Validate(data);

            if (!results.IsValid)
            {
                string allMessages = results.ToString("~");
                return BadRequest(allMessages);
            }*/


            if (!ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(
                    new {
                        Errors = ModelState.ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage)
                        )
                    }
                );
                return BadRequest(json);
            }

            try
            {
                return new OkObjectResult(_app.Insert(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] EntityDTO data)
        {
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
    }
}
