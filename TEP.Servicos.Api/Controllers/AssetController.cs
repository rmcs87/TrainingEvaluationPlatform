using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Servicos.Api.Controllers
{
    /// <summary>
    /// Controls requsitions for Assets manipulation. ControllerBase Crud functionalities are enough.
    /// </summary>
    public class AssetController : ControllerBase<Asset, AssetDTO>
    {
        public AssetController(IAppBase<Asset, AssetDTO> app) : base(app)
        {
        }
    }
}
