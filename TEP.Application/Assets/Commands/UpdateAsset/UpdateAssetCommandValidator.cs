using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetComamnd>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAssetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(a => a.FilePath)
                .NotEmpty().WithMessage("File Path is required.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Assets.AllAsync(a => a.Name != name);
        }
    }
}
