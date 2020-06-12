using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data.Repositories
{
    public class InteractionRepository : RepositoryBase<Interaction>, IInteractionRepository
    {
        public InteractionRepository(Context context) : base(context)
        {
        }
    }
}
