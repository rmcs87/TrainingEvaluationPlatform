using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Assets.Queries.GetAsset;
using TEP.Application.Common.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Queries.ListAssets
{
    public class ListAssetsQueryHandler : IRequestHandler<ListAssetsQuery, IEnumerable<AssetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListAssetsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDTO>> Handle(ListAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await _context.Assets
                .AsNoTracking()
                .Include(a => a.AssetCategories).ThenInclude(c => c.Category)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var assetDTOs = _mapper.Map<List<AssetDTO>>(assets);

            return assetDTOs;
        }
    }
}
