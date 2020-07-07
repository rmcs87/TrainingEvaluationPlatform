using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Infra.AuthProvider.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string message) : base(message)
        {
        }
    }
}
