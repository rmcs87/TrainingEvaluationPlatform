using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class TrainningSessionService : BaseService<TrainningSession>, ITrainningSessionService
    {
        public TrainningSessionService(IBaseRepository<TrainningSession> repository) : base(repository)
        {
        }
    }
}
