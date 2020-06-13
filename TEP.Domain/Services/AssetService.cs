using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        public AssetService(IBaseRepository<Asset> repository) : base(repository)
        {
        }
    }
}
