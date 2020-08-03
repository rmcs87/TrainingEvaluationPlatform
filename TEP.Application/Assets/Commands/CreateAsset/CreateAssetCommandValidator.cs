using FluentValidation;
using TEP.Application.Common.CustomValidators;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IFileService _fileService;

        public CreateAssetCommandValidator(IFileServiceFactory fileServiceFactory)
        {
            _fileService = fileServiceFactory.Create<FileAssetOptions>();
            
            RuleFor(a => a.Name)
                .AssetNameValidation();

            RuleFor(a => a.FileURI)
                .FilePathValidation();

            RuleFor(a => a.Image)
                .ImageValidation()
                .SetValidator(new FileOptionsValidator(_fileService));           
        }
    }
}
