using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAssetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(a => a.FilePath)
                .MinimumLength(5).WithMessage("File Path must be ate least 5 characters.")
                .NotEmpty().WithMessage("File Path is required.");

            RuleFor(a => a.Image)
                .NotNull().WithMessage("Image File is required.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Assets.AllAsync(a => a.Name != name);
        }
    }
}
