using Microsoft.Extensions.DependencyInjection;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.DateTimeService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDateTime(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            return services;
        }
    }
}
