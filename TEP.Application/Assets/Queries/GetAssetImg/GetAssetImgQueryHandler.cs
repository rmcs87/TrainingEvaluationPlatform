using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Queries.GetAssetImg
{
    public class GetAssetImgQueryHandler : IRequestHandler<GetAssetImgQuery, AssetImgDTO>
    {
        private readonly IFileService _fileService;

        public GetAssetImgQueryHandler(IFileServiceFactory fileServiceFactory)
        {
            _fileService = fileServiceFactory.Create<FileAssetOptions>();
        }

        public async Task<AssetImgDTO> Handle(GetAssetImgQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ServiceResponse<string> filePathResponse = _fileService.GetFilePath(request.ImgName);
                ServiceResponse<bool> fileExistssResponse = _fileService.FileExists(filePathResponse.Data);

                if (!filePathResponse.Success || !fileExistssResponse.Data || !fileExistssResponse.Success)
                    throw new NotFoundException(filePathResponse.Message);

                var img = new AssetImgDTO
                {
                    FilePath = filePathResponse.Data,
                    MimeType = GetMimeType(request.ImgName)
                };

                return img;
            }
            catch (NotFoundException nfe)
            {
                throw nfe;
            }
        }

        private static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".dnct", "application/dotnetcoretutorials");
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

       
    }
}
