using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService<FileAssetOptions> _fileService;

        public DeleteAssetCommandHandler(IApplicationDbContext context, IFileService<FileAssetOptions> fileHandler)
        {
            _context = context;
            _fileService = fileHandler;
        }

        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            string imgName;
            var asset = await _context.Assets.Where(l => l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);            

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

            imgName = asset.ImgPath;

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync(cancellationToken);

            _fileService.RemoveFile(imgName);

            return Unit.Value;
        }
    }
}
