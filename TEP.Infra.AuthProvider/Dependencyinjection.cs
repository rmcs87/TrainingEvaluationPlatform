﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.AuthProvider
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddAuthProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityServer>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITokenService, TokenServer>();

            return services;
        }
    }
}