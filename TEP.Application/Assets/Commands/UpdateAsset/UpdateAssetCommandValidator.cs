using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.CustomValidators;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetComamnd>
    {
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;

        public UpdateAssetCommandValidator(IFileServiceFactory fileServiceFactory)
        {
            _fileServiceFactory = fileServiceFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();

            RuleFor(a => a.Name)
                .AssetNameValidation();

            RuleFor(a => a.FilePath)
                .FilePathValidation();

            RuleFor(a => a.Image)
                .SetValidator(new FileOptionsValidator(_fileService))
                .Unless(a => a != null);

        }
    }
}
