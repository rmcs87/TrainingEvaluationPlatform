using FluentValidation;
using TEP.Appication.DTO;

namespace TEP.Appication.Validators
{
    public class AssetDTOValidator : AbstractValidator<AssetDTO>
    {
        public AssetDTOValidator() : base()
        {
            RuleFor(asset => asset.Name).NotEmpty().NotNull();            
            RuleFor(asset => asset.FilePath).NotEmpty().NotNull();

            When(asset => asset.Id == 0, () => {
                RuleFor(asset => asset.Image).NotNull();
                RuleFor(asset => asset.ImgPath).Empty();
            }).Otherwise(() =>
            {
                RuleFor(asset => asset.ImgPath).NotEmpty();
                RuleFor(asset => asset.Image).Null();
            });

        }
    }
}
