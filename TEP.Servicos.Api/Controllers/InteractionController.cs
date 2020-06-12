using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Servicos.Api.Controllers
{
    public class InteractionController : ControllerBase<Interaction, InteractionDTO>
    {
        public InteractionController(IAppBase<Interaction, InteractionDTO> app) : base(app)
        {
        }
    }
}
