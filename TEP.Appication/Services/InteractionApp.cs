using AutoMapper;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class InteractionApp : ServiceAppBase<Interaction, InteractionDTO>, IInteractionApp
    {
        public InteractionApp(IServiceBase<Interaction> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
