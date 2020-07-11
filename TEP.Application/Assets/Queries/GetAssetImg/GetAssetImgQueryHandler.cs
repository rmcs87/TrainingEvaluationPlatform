using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Application.Assets.Queries.GetAssetImg
{
    public class GetAssetImgQueryHandler : IRequestHandler<GetAssetImgQuery, AssetImgDTO>
    {
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;

        public GetAssetImgQueryHandler(IFileServiceFactory fileServiceFactory)
        {
            _fileServiceFactory = fileServiceFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();
        }

        public async Task<AssetImgDTO> Handle(GetAssetImgQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var img = new AssetImgDTO {
                    FilePath = await _fileService.GetFilePath(request.ImgName),
                    MimeType = GetMimeType(request.ImgName)
                };

                if (!File.Exists(img.FilePath))
                    throw new Exception();

                return img;
            }
            catch (Exception)
            {
                throw new NotFoundException("File not found.");
            }

        }

        private string GetMimeType(string fileName)
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
