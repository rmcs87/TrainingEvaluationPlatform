using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileHandler _fileHandler;

        public CreateAssetCommandValidator(IApplicationDbContext context, IFileHandler fileHandler)
        {
            _context = context;
            _fileHandler = fileHandler;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(a => a.FilePath)
                .NotEmpty().WithMessage("File Path is required.");

            RuleFor(a => a.Image)
                .NotNull().WithMessage("Image File Path is required.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Assets.AllAsync(a => a.Name != name);
        }
    }
}
