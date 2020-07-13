using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Application.Common.CustomValidators
{
    public class FileOptionsValidator : PropertyValidator
    {
        private readonly IFileService _fileService;

        public FileOptionsValidator(IFileService fileService) 
            : base("{Errors}.")
        {
            _fileService = fileService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var file = context.PropertyValue as IFormFile;
            try
            {
                _fileService.ValidateFile(file);
                return true;
            }
            catch (Exception e)
            {
                context.MessageFormatter.AppendArgument("Errors", e.Message);
                return false;
            }
        }
    }

}
