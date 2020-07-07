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

namespace TEP.Infra.AuthProvider
{
    public class TokenServer : ITokenService
    {        
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;

        public TokenServer(IIdentityService identityService, IConfiguration configuration)
        {
            _identityService = identityService;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string userId)
        {
            var user = await _identityService.GetUserAsync(userId);

            var tokenHandler = new JwtSecurityTokenHandler();

            var keyLocation = Path.Combine(Environment.CurrentDirectory, _configuration["keyFileName"]);

            var key = KeyGenerator.Loadkey(keyLocation);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        

    }
}
