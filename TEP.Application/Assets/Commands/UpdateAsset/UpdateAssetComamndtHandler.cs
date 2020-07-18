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
        private readonly IFileServiceFactory _fileServiceFactory;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public UpdateAssetComamndtHandler(IApplicationDbContext context, IFileServiceFactory fileFactory, IMapper mapper)
        {
            _context = context;
            _fileServiceFactory = fileFactory;
            _fileService = _fileServiceFactory.Create<FileAssetOptions>();
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAssetComamnd request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets
                .Include(a => a.AssetCategories)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

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
