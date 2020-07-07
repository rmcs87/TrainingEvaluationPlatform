using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Application.Auth.Commands
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Username is Required");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is Required");
        }
    }
}
