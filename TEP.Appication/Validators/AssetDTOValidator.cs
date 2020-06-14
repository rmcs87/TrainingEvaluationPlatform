using FluentValidation;
using TEP.Appication.DTO;

namespace TEP.Appication.Validators
{
    public class AssetDTOValidator : AbstractValidator<AssetDTO>
    {
        public AssetDTOValidator() : base()
        {
            RuleFor(asset => asset.Name).NotEmpty().NotNull();
            RuleFor(asset => asset.ImgPath).NotEmpty().NotNull();
            RuleFor(asset => asset.FilePath).NotEmpty().NotNull();
        }
    }
}
