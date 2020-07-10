using Microsoft.Extensions.DependencyInjection;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Infra.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileService(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileServer>();
            services.AddTransient<IFileServiceFactory, FileServerFactory>();

            return services;
        }
    }
}
