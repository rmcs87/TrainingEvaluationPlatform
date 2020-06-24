﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEP.Services.AuthProvider.Models;
using TEP.Services.AuthProvider.Repositories;
using TEP.Services.AuthProvider.Services;

namespace TEP.Servicos.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    [Authorize]    
    public class AuthController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User data)
        {
            // Recupera o usuário
            var user = UserRepository.Get(data.Username, data.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados (o proprio usuario, sem a senha !!!!!)
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("auth_test")]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> TestPermissions([FromBody]User data)
        {
            return new OkResult();        
        }

    }
}