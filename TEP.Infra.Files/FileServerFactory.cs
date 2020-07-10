using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Infra.Files
{
    public class FileServerFactory : IFileServiceFactory
    {
        private readonly IFileService _fileService;

        public FileServerFactory(IFileService fileService)
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
