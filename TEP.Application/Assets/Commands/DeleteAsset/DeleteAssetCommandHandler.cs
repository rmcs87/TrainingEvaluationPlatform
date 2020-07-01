using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileHandler _fileHandler;

        public DeleteAssetCommandHandler(IApplicationDbContext context, IFileHandler fileHandler)
        {
            _context = context;
            _fileHandler = fileHandler;
        }

        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets.Where(l => l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

            _context.Assets.Remove(asset);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
