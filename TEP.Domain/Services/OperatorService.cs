using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class OperatorService : BaseService<Operator>, IOperatorService
    {
        public OperatorService(IBaseRepository<Operator> repository) : base(repository)
        {
        }
    }
}
