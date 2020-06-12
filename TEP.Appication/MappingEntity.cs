using AutoMapper;
using System;
using System.Linq;
using TEP.Appication.DTO;
using TEP.Domain.Entities;
using TEP.Domain.Entities.Assets;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Appication
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            //SimpleAsset
            CreateMap<SimpleAsset, SimpleAssetDTO>()
                .ReverseMap();            
                
            //Interaction
            CreateMap<Interaction, InteractionDTO>()
                .ForMember(dest => dest.EstimatedTime, act => act.MapFrom(src => src.EstimatedTime.Seconds))
                .ForMember(dest => dest.TimeLimit, act => act.MapFrom(src => src.TimeLimit.Seconds))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description.Text))
                .ForMember(dest => dest.Target, act => act.MapFrom(src => src.Target.Name))
                .ForMember(dest => dest.Source, act => act.MapFrom(src => src.Source.Name))
            .ReverseMap()
                .ConvertUsing(x => new Interaction(
                    x.Categories.Select(x => (Category)Enum.Parse(typeof(Category), x)),
                    (Act)Enum.Parse(typeof(Act), x.Act),
                    new Description(x.Description),
                    new Duration(x.EstimatedTime),
                    new Duration(x.TimeLimit),
                    new SimpleAsset(x.Source, ""),
                    new SimpleAsset(x.Source, "")
                    )
                );

            //Operator
            CreateMap<Operator, OperatorDTO>()
                .ReverseMap();            

            //Step
            CreateMap<Step, StepDTO>()
                .Include<LeafStep, LeafStepDTO>()
                .Include<RecursiveStep, RecursiveStepDTO>()
                .ForMember(dest => dest.ExpectedDuration, act => act.MapFrom(src => src.ExpectedDuration.Seconds))
                .ForMember(dest => dest.LimitDuration, act => act.MapFrom(src => src.LimitDuration.Seconds))
                .ForMember(dest => dest.ExecutionTime, act => act.MapFrom(src => src.ExecutionTime.Seconds))
            .ReverseMap()
                .Include<LeafStepDTO, LeafStep>()
                .Include<RecursiveStepDTO, RecursiveStep>()
                .ForMember(dest => dest.ExpectedDuration, act => act.MapFrom(src => new Duration(src.ExpectedDuration) ))
                .ForMember(dest => dest.LimitDuration, act => act.MapFrom(src => new Duration(src.LimitDuration)))
                .ForMember(dest => dest.ExecutionTime, act => act.MapFrom(src => new Duration(src.ExecutionTime)));

            //LeafStep
            CreateMap<LeafStep, LeafStepDTO>()
                .ForMember(dest => dest.InteractionDTO, act => act.MapFrom(src => src.Interaction))
            .ReverseMap()
                .ForMember(dest => dest.Interaction, act => act.MapFrom(src => src.InteractionDTO));

            //RecursiveStep
            CreateMap<RecursiveStep, RecursiveStepDTO>()
                .ForMember(dest => dest.StepDTOs, act => act.MapFrom(src => src.SubSteps))
            .ReverseMap()
                .ForMember(dest => dest.SubSteps, act => act.MapFrom(src => src.StepDTOs));

            //Procedure
            CreateMap<Procedure, ProcedureDTO>()
                .ForMember(dest => dest.Expected, act => act.MapFrom(src => src.Expected.Seconds))
                .ForMember(dest => dest.Execution, act => act.MapFrom(src => src.Execution.Seconds))
                .ForMember(dest => dest.Limit, act => act.MapFrom(src => src.Limit.Seconds))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description.Text))
            .ReverseMap()
                .ForMember(dest => dest.Expected, act => act.MapFrom(src => new Duration(src.Expected)))
                .ForMember(dest => dest.Execution, act => act.MapFrom(src => new Duration(src.Execution)))
                .ForMember(dest => dest.Limit, act => act.MapFrom(src => new Duration(src.Limit)))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => new Description(src.Description)))
                .ForCtorParam("description", opt => opt.MapFrom(src => new Description(src.Description) ));

            //Supervisor
            CreateMap<Supervisor, SupervisorDTO>()
            .ReverseMap();

            //TrainningSession
            CreateMap<TrainningSession, TrainningSessionDTO>()
                .ForMember(dest => dest.Score, act => act.MapFrom(src => src.Performance.Score))
                .ForMember(dest => dest.TimeExecution, act => act.MapFrom(src => src.Performance.TimeExecution.Seconds))
                .ReverseMap()
                .ForMember(dest => dest.Performance, act => act.MapFrom(src => new Performance()
                {
                    TimeExecution = new Duration(src.TimeExecution),
                    Score = src.Score
                })) ;
        }
    }
}
