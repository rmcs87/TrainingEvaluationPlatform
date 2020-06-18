using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Servicos.Api.Controllers
{
    public class AssetController : ControllerBase<Asset, AssetDTO>
    {
        public AssetController(IAppBase<Asset, AssetDTO> app) : base(app)
        {
        }
    }
}
