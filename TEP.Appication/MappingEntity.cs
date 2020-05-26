using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;

namespace TEP.Appication
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            //Interaction
            CreateMap<Interaction, InteractionDTO>()
                .ReverseMap();
            //Operator
            CreateMap<Operator, OperatorDTO>()
                .ReverseMap();
            //Procedure
            CreateMap<Procedure, ProcedureDTO>()
                .ReverseMap();
            //Step
            CreateMap<Step, StepDTO>()
                .ReverseMap();
            //Supervisor
            CreateMap<Supervisor, SupervisorDTO>()
                .ReverseMap();
            //TrainningSession
            CreateMap<TrainningSession, TrainningSessionDTO>()
                .ForMember(dest => dest.Score, act => act.MapFrom(src => src.Performance.Score))
                .ForMember(dest => dest.TimeExecution, act => act.MapFrom(src => src.Performance.TimeExecution))
                .ReverseMap()
                .ForMember(dest => dest.Performance, act => act.MapFrom(src => new Performance()
                {
                    TimeExecution = new Shared.ValueObjects.Duration(src.TimeExecution),
                    Score = src.Score
                })) ;
        }
    }
}
