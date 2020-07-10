using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TEP.Application.Common.Interfaces
{
    public interface IFileService
    {     
        Task<string> SaveFile(IFormFile data);
        void RemoveFile(string path);
        Task<byte[]> GetFileBytes(string path);

        public IFileOptions Options { get; set; }
    }
}
