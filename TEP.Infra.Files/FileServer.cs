using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Infra.Files.Exceptions;

namespace TEP.Infra.Files
{
    public class FileServer : IFileService
    {
        
        private readonly ILogger<FileServer> _logger;

        public FileServer(ILogger<FileServer> logger)
        {
            _logger = logger;
            Options = new DefaultOptions();
        }

        public IFileOptions Options { get; set; }

        public async Task<byte[]> GetFileBytes(string fileName)
        {
            try
            {
                var filePath = FileHelper.CombinePathAndName(Options.BasePath, fileName);

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
                _logger.LogError(ex, "File Retrieval: {FileName} {BasePath}", fileName, Options.BasePath);

                throw new FileRetrievalException("File Could not be recovered.");
            }
        }

        public void RemoveFile(string fileName)
        {
            try
            {
                var filePath = FileHelper.CombinePathAndName(Options.BasePath, fileName);
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Removal: {FileName} {BasePath}", fileName, Options.BasePath);

                throw new FileRemovalException("File Could not be deleted");
            }
        }

        public async Task<string> SaveFile(IFormFile data)
        {
            try
            {
                string fileName = FileHelper.GetUniqueName(Options.NameSalt, data.FileName);
                FileHelper.ValidateFile(data, Options);

                await FileHelper.ProcessFile(data, Options, fileName);

                return fileName;
            }
            catch (FileCreationException fileException)
            {
                _logger.LogWarning(fileException, "File Creation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Creation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                throw new FileCreationException("File Could not be saved.");
            }
        }
    }
}
