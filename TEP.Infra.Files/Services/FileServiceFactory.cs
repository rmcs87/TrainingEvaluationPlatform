using TEP.Application.Common.Interfaces;
using TEP.Infra.Files.Options;

namespace TEP.Infra.Files
{
    public class FileServiceFactory : IFileServiceFactory
    {
        private readonly IFileService _fileService;

        public FileServiceFactory(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IFileService Create(FileProfile profile)
        {
            switch (profile)
            {
                case FileProfile.AssetImage:
                    _fileService.Options = new FileAssetOptions();
                    break;
            }

            return _fileService;
        }
    }
}
