using AutoMapper;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class ProcedureApp : ServiceAppBase<Procedure, ProcedureDTO>, IProcedureApp
    {
        public ProcedureApp(IServiceBase<Procedure> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
