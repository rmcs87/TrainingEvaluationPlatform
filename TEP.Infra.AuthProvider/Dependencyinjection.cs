using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.AuthProvider
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddAuthProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
