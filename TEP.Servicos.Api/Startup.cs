using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TEP.Infra.Data.Contexto;
using TEP.Infra.IoC;
using TEP.Appication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using System.Reflection;
using TEP.Appication.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TEP.Services.AuthProvider.Services;
using TEP.Shared;

namespace TEP.Servicos.Api
{
    public class Startup
    {

        /*public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/


        //To load and inject the configuration settings from multiple Configuration Files
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile("privateSettings.json", true, true);

            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IConfiguration>(Configuration);

            //services.AddDbContext<Context>(o => o.UseSqlServer(Configuration.GetConnectionString("teps")));
            //Change the migrations assembly, because when working with a DbContext that is in a separate project from your web app project it is necessary            
            var connectionString = Configuration.GetValue<string>("ConnectionStrings:teps");
            services.AddDbContext<Context>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("TEP.Servicos.Api")));

            DependencyInjector.Register(services);

            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()), typeof(Startup));
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AssetDTOValidator>());

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserPolicies.AdministratorRights.ToString(),
                     policy => policy.RequireRole(UserRoles.Admin.ToString()));
                options.AddPolicy(UserPolicies.ManagerRights.ToString(),
                     policy => policy.RequireRole(UserRoles.Admin.ToString(), 
                                                  UserRoles.Manager.ToString()));
                options.AddPolicy(UserPolicies.SupervisorRights.ToString(),
                     policy => policy.RequireRole(UserRoles.Admin.ToString(), 
                                                  UserRoles.Manager.ToString(), 
                                                  UserRoles.Supervisor.ToString()));
            });

            var key = TokenService.Loadkey();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
