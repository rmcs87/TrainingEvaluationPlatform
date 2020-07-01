using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Application.Common.Exceptions;
using TEP.Application.Common.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Queries.GetAsset
{
    public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, AssetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAssetQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AssetDTO> Handle(GetAssetQuery request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets.FindAsync(request.Id);

            if (asset == null)
                throw new NotFoundException(nameof(Asset), request.Id);

            var dto = _mapper.Map<AssetDTO>(asset);

            return dto;
        }
    }
}
