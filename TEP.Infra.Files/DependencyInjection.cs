using Microsoft.Extensions.DependencyInjection;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileService(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFileServiceFactory, FileServiceFactory>();

            return services;
        }
    }
}
