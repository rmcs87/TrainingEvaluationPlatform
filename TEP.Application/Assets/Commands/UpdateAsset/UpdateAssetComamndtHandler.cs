using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFileService _fileService;

        public UpdateAssetComamndtHandler(IApplicationDbContext context, IFileServiceFactory fileFactory)
        {
            _context = context;
            _fileService = fileFactory.Create<FileAssetOptions>();
        }

        public async Task<Unit> Handle(UpdateAssetComamnd request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets
                .Include(a => a.AssetCategories)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (asset == null)
            {
                throw new NotFoundException(nameof(Asset), request.Id);
            }

            if (request.Image != null)
            {
                var newImgPath = await _fileService.SaveFile(request.Image);
                _fileService.RemoveFile(asset.IconPath);
                asset.UpdateIcon(newImgPath);
            }

            asset.ChangeName(request.Name);
            asset.UpdateFileURI(request.FileURI);

            asset.RemoveAllCategories();
            foreach (var categoryId in request.CategoriesIds)
            {
                asset.AddCategoryById(categoryId);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
