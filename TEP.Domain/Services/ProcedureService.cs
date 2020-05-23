using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class ProcedureService : BaseService<Procedure>, IProcedureService
    {
        public ProcedureService(IBaseRepository<Procedure> repository) : base(repository)
        {
        }
    }
}
