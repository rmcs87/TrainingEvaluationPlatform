using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data.Repositories
{
    public class SupervisorRepository : RepositoryBase<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(Context context) : base(context)
        {
        }
    }
}
