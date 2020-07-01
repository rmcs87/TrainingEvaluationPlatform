using Microsoft.Extensions.DependencyInjection;
using System;
using TEP.Application.Interfaces;
using TEP.Application.Services;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;
using TEP.Domain.Services;
using TEP.Infra.Data.Repositories;

namespace TEP.Infra.IoC
{
    public class DependencyInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {
            //Necessário os Steps aqui, ou apenas o Procedure?
            //Application
            svcCollection.AddScoped(typeof(IAppBase<,>), typeof(ServiceAppBase<,>));
            svcCollection.AddScoped<IAssetApp, AssetApp>();
            svcCollection.AddScoped<IInteractionApp, InteractionApp>();
            /*svcCollection.AddScoped<ILeafStepApp, LeafStepApp>();*/
            svcCollection.AddScoped<IOperatorApp, OperatorApp>();
            svcCollection.AddScoped<IProcedureApp, ProcedureApp>();
            /*svcCollection.AddScoped<IRecursiveStepApp, RecursiveStepApp>();*/
            /*svcCollection.AddScoped<IStepApp, StepApp>();*/
            svcCollection.AddScoped<ISupervisorApp, SupervisorApp>();
            svcCollection.AddScoped<ITrainningSessionApp, TrainningSessionApp>();

            //Domain
            svcCollection.AddScoped(typeof(IServiceBase<>), typeof(BaseService<>));
            svcCollection.AddScoped<IAssetService, AssetService>();
            svcCollection.AddScoped<IInteractionService, InteractionService>();
            svcCollection.AddScoped<IOperatorService, OperatorService>();
            svcCollection.AddScoped<IProcedureService, ProcedureService>();
            svcCollection.AddScoped<ISupervisorService, SupervisorService>();
            svcCollection.AddScoped<ITrainningSessionService, TrainningSessionService>();

            //Repositories
            svcCollection.AddScoped(typeof(IBaseRepository<>), typeof(RepositoryBase<>));
            svcCollection.AddScoped<IAssetRepository, AssetRepository>();
            svcCollection.AddScoped<IInteractionRepository, InteractionRepository>();
            svcCollection.AddScoped<IOperatorRepository, OperatorRepository>();
            svcCollection.AddScoped<IProcedureRepository, ProcedureRepository>();
            svcCollection.AddScoped<ISupervisorRepository, SupervisorRepository>();
            svcCollection.AddScoped<ITrainningSessionRepository, TrainningSessionRepository>();
        }
    }
}
