using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;

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

            ServiceResponse<bool> response = _fileService.ValidateFile(file);
            _fileService.ValidateFile(file);
            if(!response.Data)
                context.MessageFormatter.AppendArgument("Errors", response.Message);

            return response.Data;
        }
    }

}
