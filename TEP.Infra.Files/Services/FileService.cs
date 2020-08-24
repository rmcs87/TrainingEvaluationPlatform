using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
using TEP.Infra.Files.Exceptions;

namespace TEP.Infra.Files
{
    public class FileService : IFileService
    {
        
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
            Options = new DefaultOptions();
        }

        public IFileOptions Options { get; set; }

        public ServiceResponse<bool> FileExists(string filePath)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                response.Data = File.Exists(filePath);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Existence: {FileName} {BasePath}", filePath, Options.BasePath);
                response.Success = false;
                response.Message = "File existence could not be verified.";
            }

            return response;
        }

        public async Task<ServiceResponse<byte[]>> GetFileBytes(string fileName)
        {
            ServiceResponse<byte[]> response = new ServiceResponse<byte[]>();
            try
            {
                var filePath = FileHelper.CombinePathAndName(Options.BasePath, fileName);

                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;

                response.Data = memoryStream.ToArray();
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Retrieval: {FileName} {BasePath}", fileName, Options.BasePath);
                response.Success = false;
                response.Message = "File Could not be recovered.";
            }
            return response;
        }
        
        public ServiceResponse<string> GetFilePath(string fileName)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var filePath = FileHelper.CombinePathAndName(Options.BasePath, fileName);
                response.Data =  filePath;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File path Retrieval: {FileName} {BasePath}", fileName, Options.BasePath);
                response.Message = "File path Could not be recovered.";
                response.Success = false;
            }
            return response;
        }

        public ServiceResponse<bool> RemoveFile(string fileName)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                var filePath = FileHelper.CombinePathAndName(Options.BasePath, fileName);
                File.Delete(filePath);
                response.Success = true;
                response.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Removal: {FileName} {BasePath}", fileName, Options.BasePath);
                response.Success = false;
                response.Message = "File Could not be Removed.";
            }
            return response;
        }

        public ServiceResponse<bool> ValidateFile(IFormFile data)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                FileHelper.ValidateFile(data, Options);
                response.Success = true;
                response.Data = true;
            }
            catch (FileCreationException fcex)
            {
                _logger.LogError(fcex, "File Validation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                response.Success = false;
                response.Message = fcex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Validation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                response.Success = false;
                response.Message = "File Could not be Verified due to unknown problmens.";
            }
            return response;            
        }

        public async Task<ServiceResponse<string>> SaveFile(IFormFile data)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                string fileName = FileHelper.GetUniqueName(Options.NameSalt, data.FileName);

                await FileHelper.ProcessFile(data, Options, fileName);

                response.Data =  fileName;
                response.Success = true;
            }
            catch (FileCreationException fileException)
            {
                _logger.LogWarning(fileException, "File Creation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                response.Success = false;
                response.Message = fileException.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File Creation: {FileName} {BasePath}", data.FileName, Options.BasePath);
                response.Success = false;
                response.Message = "File Could not be saved due to unknown errror.";
            }
            return response;
        }
    }
}
