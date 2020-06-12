using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class InteractionService : BaseService<Interaction>, IInteractionService
    {
        public InteractionService(IBaseRepository<Interaction> repository) : base(repository)
        {
        }
    }
}
