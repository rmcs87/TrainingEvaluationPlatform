using Microsoft.Extensions.DependencyInjection;
using System;
using TEP.Appication.Interfaces;
using TEP.Appication.Services;
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
            svcCollection.AddScoped<ISimpleAssetApp, SimpleAssetApp>();
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
            svcCollection.AddScoped<ISimpleAssetService, SimpleAssetService>();
            svcCollection.AddScoped<IInteractionService, InteractionService>();
            svcCollection.AddScoped<IOperatorService, OperatorService>();
            svcCollection.AddScoped<IProcedureService, ProcedureService>();
            svcCollection.AddScoped<ISupervisorService, SupervisorService>();
            svcCollection.AddScoped<ITrainningSessionService, TrainningSessionService>();

            //Repositories
            svcCollection.AddScoped(typeof(IBaseRepository<>), typeof(RepositoryBase<>));
            svcCollection.AddScoped<ISimpleAssetRepository, SimpleAssetRepository>();
            svcCollection.AddScoped<IInteractionRepository, InteractionRepository>();
            svcCollection.AddScoped<IOperatorRepository, OperatorRepository>();
            svcCollection.AddScoped<IProcedureRepository, ProcedureRepository>();
            svcCollection.AddScoped<ISupervisorRepository, SupervisorRepository>();
            svcCollection.AddScoped<ITrainningSessionRepository, TrainningSessionRepository>();
        }
    }
}
