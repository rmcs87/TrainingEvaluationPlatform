using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;

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

        public Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityService.ValidateLogin(request.UserName, request.Password);
                return _tokenService.GenerateTokenAsync(user.Id.ToString());
            }
            catch (Exception ie)
            {
                throw new AuthenticateCommandExcpetion(ie.Message);
            }
        }
    }
}
