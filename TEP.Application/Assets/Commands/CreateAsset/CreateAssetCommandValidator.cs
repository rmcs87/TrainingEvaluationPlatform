using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;

        public CreateAssetCommandValidator(IApplicationDbContext context, IFileServiceFactory fileServiceFactory)
        {
            _context = context;
            _fileServiceFactory = fileServiceFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();
            

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(a => a.FilePath)
                .MinimumLength(5).WithMessage("File Path must be ate least 5 characters.")
                .NotEmpty().WithMessage("File Path is required.");

            RuleFor(a => a.Image)
                .NotNull().WithMessage("Image File is required.")
                .Custom(BeValidFile);
            
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Assets.AllAsync(a => a.Name != name);
        }

        private void BeValidFile(IFormFile image, CustomContext context)
        {
            try
            {
                _fileService.ValidateFile(image);
            }
            catch (Exception e)
            {
                context.AddFailure(e.Message);
            }            
        }
    }
}
