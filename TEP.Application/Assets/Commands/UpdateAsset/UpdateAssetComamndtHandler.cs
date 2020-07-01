using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetComamndtHandler : IRequestHandler<UpdateAssetComamnd>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileHandler _fileHandler;

        public UpdateAssetComamndtHandler(IApplicationDbContext context, IFileHandler fileHandler)
        {
            _context = context;
            _fileHandler = fileHandler;
        }

        public async Task<Unit> Handle(UpdateAssetComamnd request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets.FindAsync(request.Id);

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

            asset.Name = request.Name;
            asset.ImgPath = "updated image path"; //Vai depender aqui se houve mudança no arquivo....
            asset.FilePath = request.FilePath;


            return Unit.Value;
        }
    }
}
