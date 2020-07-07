using System;

namespace TEP.Application.Auth.Commands
{
    public class AuthenticateCommandExcpetion : Exception
    {
        public AuthenticateCommandExcpetion(string message) : base(message)
        {
        }
    }
}
