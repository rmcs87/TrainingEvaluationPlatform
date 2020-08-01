using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
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
            var asset = await _context.Assets
                .AsNoTracking()
                .Include(a => a.AssetCategories).ThenInclude(ac => ac.Category)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (asset == null)
            {
                throw new NotFoundException(nameof(Asset), request.Id);
            }

            var dto = _mapper.Map<AssetDTO>(asset);

            dto.IconPath = $"api/asset/image/{asset.IconPath}";

            return dto;
        }
    }
}
