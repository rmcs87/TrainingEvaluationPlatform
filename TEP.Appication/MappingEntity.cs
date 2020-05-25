using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Domain.Entities;

namespace TEP.Appication
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            //Interaction
            CreateMap<Interaction, InteractionDTO>();
            CreateMap<InteractionDTO, Interaction>();
            //Operator
            CreateMap<Operator, OperatorDTO>();
            CreateMap<OperatorDTO, Operator>();
            //Procedure
            CreateMap<Procedure, ProcedureDTO>();
            CreateMap<ProcedureDTO, Procedure>();
            //Step
            CreateMap<Step, StepDTO>();
            CreateMap<StepDTO, Step>();
            //Supervisor
            CreateMap<Supervisor, SupervisorDTO>();
            CreateMap<SupervisorDTO, Supervisor>();
            //TrainningSession
            CreateMap<TrainningSession, TrainningSessionDTO>();
            CreateMap<TrainningSessionDTO, TrainningSession>();
        }
    }
}
