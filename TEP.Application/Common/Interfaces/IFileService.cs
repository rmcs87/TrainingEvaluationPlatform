using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<ServiceResponse<string>> SaveFile(IFormFile data);
        ServiceResponse<bool> RemoveFile(string path);
        Task<ServiceResponse<byte[]>> GetFileBytes(string path);
        ServiceResponse<string> GetFilePath(string fileName);
        ServiceResponse<bool> FileExists(string filePath);
        ServiceResponse<bool> ValidateFile(IFormFile data);
        public IFileOptions Options { get; set; }
    }
}
