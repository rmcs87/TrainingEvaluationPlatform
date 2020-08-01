using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Infra.Files.Exceptions;

namespace TEP.Infra.Files
{
    public static class FileHelper
    {
        public static async Task ProcessFile(IFormFile data, IFileOptions options, string fileName)
        {

            var filePath = FileHelper.CombinePathAndName(options.BasePath, fileName);

            try
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    await data.CopyToAsync(stream);
                }

            }
            catch (Exception ex)
            {
                throw new IOException($"upload failed. Please contact the Help Desk for support. Error: {ex.HResult}");
            }
        }

        public static void ValidateFile(IFormFile data, IFileOptions options)
        {
            if (!FileHelper.IsFileExtensionValid(data.FileName, options.SupportedFilesExtension))
            {
                throw new FileCreationException($"File type is not Accepeted. Use {String.Join(",", options.SupportedFilesExtension)}");
            }
            if (data.Length == 0)
            {
                throw new FileCreationException($"File is empty.");
            }
            if (data.Length > options.SizeLimit)
            {
                throw new FileCreationException($"File exceeds Maximum Size of {options.SizeLimit}.");
            }
        }

        private static bool IsFileExtensionValid(string fileName, string[] permittedExtensions)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                return false;

            return true;
        }

        public static string GetUniqueName(string salt, string originalName)
        {
            var extension = Path.GetExtension(originalName).ToLowerInvariant();
            return salt + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + extension;
        }

        public static string CombinePathAndName(string filePath, string fileName)
        {
            return Path.Combine(filePath, fileName);
        }

    }
}
