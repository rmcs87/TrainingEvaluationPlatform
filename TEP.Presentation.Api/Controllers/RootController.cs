﻿using Microsoft.AspNetCore.Mvc;
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
                return new OkObjectResult("Welcome to TEP.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
