using Microsoft.AspNetCore.Mvc;
using System;

namespace TEP.Presentation.Api.Controllers
{
    public class RootController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Test()
        {
            try
            {
                return new OkObjectResult("HELLO TEP.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
