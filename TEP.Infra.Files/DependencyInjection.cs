using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;

namespace TEP.Infra.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileService(this IServiceCollection services)
        {
            services.AddTransient<IFileService<IFileOptions>, FileServer>();

            return services;
        }
    }
}
