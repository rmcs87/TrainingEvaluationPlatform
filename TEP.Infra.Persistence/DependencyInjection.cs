using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraPersistence(this IServiceCollection services, 
                                                                IConfiguration configuration)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStringTep")
                                        ?? configuration["ConnectionStrings:teps"];

            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(connectionString,
                               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
