using TEP.Application.Common.Interfaces;

namespace TEP.Infra.Files
{
    public class FileServiceFactory : IFileServiceFactory
    {
        private readonly IFileService _fileService;

        public FileServiceFactory(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IFileService Create<T>()
            where T : IFileOptions ,new()
        {
            _fileService.Options = new T();

            return _fileService;
        }
    }
}
