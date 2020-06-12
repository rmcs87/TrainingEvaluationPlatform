using TEP.Domain.Entities.Assets;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class SimpleAssetService : BaseService<SimpleAsset>, ISimpleAssetService
    {
        public SimpleAssetService(IBaseRepository<SimpleAsset> repository) : base(repository)
        {
        }
    }
}
