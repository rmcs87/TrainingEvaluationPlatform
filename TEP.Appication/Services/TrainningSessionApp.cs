using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class TrainningSessionApp : ServiceAppBase<TrainningSession, TrainningSessionDTO>, ITrainningSessionApp
    {
        public TrainningSessionApp(IServiceBase<TrainningSession> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
