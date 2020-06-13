using AutoMapper;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class AssetApp : ServiceAppBase<Asset, AssetDTO>, IAssetApp
    {
        public AssetApp(IServiceBase<Asset> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
