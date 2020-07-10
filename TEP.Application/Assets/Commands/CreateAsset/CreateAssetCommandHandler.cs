using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;

        public CreateAssetCommandHandler(IApplicationDbContext context, IFileServiceFactory fileFactory)
        {
            _context = context;
            _fileServiceFactory = fileFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();
        }

        public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            string imgPath = "";
            try
            {
                imgPath = await _fileService.SaveFile(request.Image);

                var asset = new Asset(request.FilePath, request.Name, imgPath);

                _context.Assets.Add(asset);

                await _context.SaveChangesAsync(cancellationToken);

                return asset.Id;
            }
            catch (Exception e)
            {
                if(!string.IsNullOrEmpty(imgPath))
                    _fileService.RemoveFile(imgPath);

                throw new CreateAssetCommandException("Sorry we had an unknown problem while Creating your Asset.", e);
            }
        }
    }
}
