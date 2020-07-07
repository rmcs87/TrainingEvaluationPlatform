using MediatR;

namespace TEP.Application.Auth.Commands
{
    public class AuthenticateCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
