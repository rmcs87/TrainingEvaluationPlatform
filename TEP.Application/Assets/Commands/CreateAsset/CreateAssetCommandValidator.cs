using FluentValidation;
using TEP.Application.Common.CustomValidators;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;

        public CreateAssetCommandValidator(IFileServiceFactory fileServiceFactory)
        {
            _fileServiceFactory = fileServiceFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();
            
            RuleFor(a => a.Name)
                .AssetNameValidation();

            RuleFor(a => a.FilePath)
                .FilePathValidation();

            RuleFor(a => a.Image)
                .ImageValidation()
                .SetValidator(new FileOptionsValidator(_fileService));           
        }
    }
}
