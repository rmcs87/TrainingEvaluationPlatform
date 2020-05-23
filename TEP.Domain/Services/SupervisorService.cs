using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class SupervisorService : BaseService<Supervisor>, ISupervisorService
    {
        public SupervisorService(IBaseRepository<Supervisor> repository) : base(repository)
        {
        }
    }
}
