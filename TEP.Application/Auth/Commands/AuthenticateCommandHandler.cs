using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;

namespace TEP.Application.Auth.Commands
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, string>
    {

        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticateCommandHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<ApplicationUser> identityResponse = await _identityService.ValidateLoginAsync(request.UserName, request.Password);
            var user = identityResponse.Data;
            ServiceResponse<string> tokenResponse = await _tokenService.GenerateTokenAsync(user.Id.ToString());
            return tokenResponse.Data;
        }
    }
}
