using AutoMapper;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class LeafStepApp : ServiceAppBase<LeafStep, LeafStepDTO>, ILeafStepApp
    {
        public LeafStepApp(IServiceBase<LeafStep> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
