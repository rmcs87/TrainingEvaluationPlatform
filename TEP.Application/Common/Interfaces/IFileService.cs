using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TEP.Application.Common.Interfaces
{
    public interface IFileService
    {     
        Task<string> SaveFile(IFormFile data);
        void RemoveFile(string path);
        Task<byte[]> GetFileBytes(string path);
        Task<string> GetFilePath(string fileName);
        bool FileExists(string filePath);
        public IFileOptions Options { get; set; }
    }
}
