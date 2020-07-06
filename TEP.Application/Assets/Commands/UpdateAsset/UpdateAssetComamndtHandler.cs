using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetComamndtHandler : IRequestHandler<UpdateAssetComamnd>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService<FileAssetOptions> _fileService;

        public UpdateAssetComamndtHandler(IApplicationDbContext context, IFileService<FileAssetOptions> fileHandler)
        {
            _context = context;
            _fileService = fileHandler;
        }

        public async Task<Unit> Handle(UpdateAssetComamnd request, CancellationToken cancellationToken)
        {
            string newImgPath = "";
            var asset = await _context.Assets.FindAsync(request.Id);
            
            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

            if(request.Image != null)
            {
                newImgPath = await _fileService.SaveFile(request.Image);
                _fileService.RemoveFile(asset.ImgPath);
            }

            asset.Name = request.Name;
            asset.ImgPath = newImgPath;
            asset.FilePath = request.FilePath;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
