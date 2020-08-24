using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;

namespace TEP.Infra.AuthProvider
{
    public class TokenService : ITokenService
    {
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;

        public TokenService(IIdentityService identityService, IConfiguration configuration)
        {
            _identityService = identityService;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> GenerateTokenAsync(string userId)
        {
            ServiceResponse<ApplicationUser> response = await _identityService.GetUserAsync(userId);
            var user = response.Data;

            var tokenHandler = new JwtSecurityTokenHandler();

            var keyLocation = Path.Combine(Environment.CurrentDirectory, _configuration["keyFileName"]);

            var key = KeyGenerator.Loadkey(keyLocation);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new ServiceResponse<string>() { Data = tokenHandler.WriteToken(token), Success = true };
        }

    }
}
