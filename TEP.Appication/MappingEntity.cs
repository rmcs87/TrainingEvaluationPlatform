using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;
using TEP.Shared;
using TEP.Shared.ValueObjects;

namespace TEP.Appication
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            //Interaction
            CreateMap<Interaction, InteractionDTO>()
                .ForMember(dest => dest.EstimatedTime, act => act.MapFrom(src => src.EstimatedTime.Seconds))
                .ForMember(dest => dest.TimeLimit, act => act.MapFrom(src => src.TimeLimit.Seconds))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description.Text))
                .ForMember(dest => dest.Target, act => act.MapFrom(src => src.Target.Name))
                .ForMember(dest => dest.Source, act => act.MapFrom(src => src.Source.Name));
            /*.ReverseMap()
            .ForMember(dest => dest.Description, act => act.MapFrom(src => new Description(src.Description)))
            .ForMember(dest => dest.TimeLimit, act => act.MapFrom(src => new Duration(src.TimeLimit)))
            .ForMember(dest => dest.EstimatedTime, act => act.MapFrom(src => new Duration(src.EstimatedTime)))
            .ForMember(dest => dest.Target, act => act.MapFrom(src => new SimpleAsset(src.Source, "") ))
            .ForMember(dest => dest.Source, act => act.MapFrom(src => new SimpleAsset(src.Source, "") ));*/
            //Operator
            CreateMap<Operator, OperatorDTO>();
                //.ReverseMap();
            //Procedure
            CreateMap<Procedure, ProcedureDTO>()
                .ForMember(dest => dest.Expected, act => act.MapFrom(src => src.Expected.Seconds))
                .ForMember(dest => dest.Execution, act => act.MapFrom(src => src.Execution.Seconds))
                .ForMember(dest => dest.Limit, act => act.MapFrom(src => src.Limit.Seconds));
                //.ReverseMap();
            //Step

            Dictionary<Type, StepType> typeDict = new Dictionary<Type, StepType>
            {
                {typeof(LeafStep),StepType.Leaf},
                {typeof(RecursiveStep),StepType.Sequential},
            };

            CreateMap<Step, StepDTO>()
                .ForMember(dest => dest.ExpectedDuration, act => act.MapFrom(src => src.ExpectedDuration.Seconds))
                .ForMember(dest => dest.LimitDuration, act => act.MapFrom(src => src.LimitDuration.Seconds))
                .ForMember(dest => dest.ExecutionTime, act => act.MapFrom(src => src.ExecutionTime.Seconds))
                //.ForMember(dest => dest., act => act.MapFrom(src => src.Description.Text))
                .ForMember(dest => dest.StepType, act => act.MapFrom(src => typeDict[src.GetType()]))
                .ForMember(dest => dest.InteractionDTO, 
                    act => act.MapFrom(src => (typeDict[src.GetType()] == StepType.Leaf) ? (src as LeafStep).Interaction : null ));
            //.ReverseMap();
            //Supervisor
            CreateMap<Supervisor, SupervisorDTO>();
                /*.ReverseMap();
            //TrainningSession
            CreateMap<TrainningSession, TrainningSessionDTO>()
                .ForMember(dest => dest.Score, act => act.MapFrom(src => src.Performance.Score))
                .ForMember(dest => dest.TimeExecution, act => act.MapFrom(src => src.Performance.TimeExecution))
                .ReverseMap()
                .ForMember(dest => dest.Performance, act => act.MapFrom(src => new Performance()
                {
                    TimeExecution = new Shared.ValueObjects.Duration(src.TimeExecution),
                    Score = src.Score
                })) ;*/
        }
    }
}
