using Microsoft.AspNetCore.Mvc;
using System;
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
                var trainnings = _app.List();
                return new OkObjectResult(trainnings);
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
                var trainnings = _app.GetById(id);
                return new OkObjectResult(trainnings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] EntityDTO data)
        {
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
