using AutoMapper;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities.Assets;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class SimpleAssetApp : ServiceAppBase<SimpleAsset, SimpleAssetDTO>, ISimpleAssetApp
    {
        public SimpleAssetApp(IServiceBase<SimpleAsset> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
