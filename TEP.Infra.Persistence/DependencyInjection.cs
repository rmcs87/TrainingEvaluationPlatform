using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TEP.Application.Common.Interfaces;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:teps");
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(connectionString,
                               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
