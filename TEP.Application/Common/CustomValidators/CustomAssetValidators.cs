using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TEP.Application.Common.CustomValidators
{
    public static class CustomAssetValidators
    {
        public static IRuleBuilderOptions<T, string> AssetNameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.");
        }

        public static IRuleBuilderOptions<T, string> FilePathValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(5).WithMessage("File Path must be ate least 5 characters.")
                .NotEmpty().WithMessage("File Path is required.");
        }
        
        public static IRuleBuilderOptions<T, IFormFile> ImageValidation<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Image File is required.");

        }


    }
}
