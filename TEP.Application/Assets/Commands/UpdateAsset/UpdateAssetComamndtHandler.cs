using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
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

            await ChangeAssetFile(request, asset);

            asset.ChangeName(request.Name);
            asset.UpdateFileURI(request.FileURI);
            UpdateCategories(request, asset);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private static void UpdateCategories(UpdateAssetComamnd request, Asset asset)
        {
            asset.RemoveAllCategories();
            foreach (var categoryId in request.CategoriesIds)
            {
                asset.AddCategoryById(categoryId);
            }
        }

        private async Task ChangeAssetFile(UpdateAssetComamnd request, Asset asset)
        {
            if (request.Image != null)
            {
                ServiceResponse<string> response = await _fileService.SaveFile(request.Image);
                var newImgPath = response.Data;
                _fileService.RemoveFile(asset.IconPath);
                asset.UpdateIcon(newImgPath);
            }
        }
    }
}
