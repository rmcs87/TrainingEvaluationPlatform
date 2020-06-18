using Microsoft.AspNetCore.Mvc;
using System;

namespace TEP.Servicos.Api.Controllers
{
    public class RootController : Controller
    {
        [HttpPost]
        [Route("")]
        public IActionResult Test()
        {
            try
            {
                return new OkObjectResult("Hello TEP.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
