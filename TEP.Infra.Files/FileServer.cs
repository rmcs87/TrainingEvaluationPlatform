using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Infra.Files.Exceptions;

namespace TEP.Infra.Files
{
    public class FileServer : IFileService<FileAssetOptions>
    {
        private readonly IFileOptions _options;
        private readonly ILogger<FileServer> _logger;

        public FileServer(IOptionsMonitor<FileAssetOptions> optionsAccessor, ILogger<FileServer> logger)
        {
            _options = optionsAccessor.CurrentValue;
            _logger = logger;
        }

        public async Task<byte[]> GetFileBytes(string fileName)
        {
            try
            {
                var filePath = FileHelper.CombinePathAndName(_options.BasePath, fileName);

                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Retrieval: {FileName} {BasePath}", fileName, _options.BasePath);

                throw new FileRetrievalException("File Could not be recovered.");
            }
        }

        public void RemoveFile(string fileName)
        {
            try
            {
                var filePath = FileHelper.CombinePathAndName(_options.BasePath, fileName);
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Removal: {FileName} {BasePath}", fileName, _options.BasePath);

                throw new FileRemovalException("File Could not be deleted");
            }
        }

        public async Task<string> SaveFile(IFormFile data)
        {
            try
            {
                string fileName = FileHelper.GetUniqueName(_options.NameSalt, data.FileName);
                FileHelper.ValidateFile(data, _options);

                await FileHelper.ProcessFile(data, _options, fileName);

                return fileName;
            }
            catch (FileCreationException fileException)
            {
                _logger.LogWarning(fileException, "File Creation: {FileName} {BasePath}", data.FileName, _options.BasePath);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Creation: {FileName} {BasePath}", data.FileName, _options.BasePath);
                throw new FileCreationException("File Could not be saved.");
            }
        }
    }
}
