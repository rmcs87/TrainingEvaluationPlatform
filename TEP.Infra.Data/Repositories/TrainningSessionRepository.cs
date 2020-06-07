using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data.Repositories
{
    public class TrainningSessionRepository : RepositoryBase<TrainningSession>, ITrainningSessionRepository
    {
        public TrainningSessionRepository(Context context) : base(context)
        {
        }
    }
}
