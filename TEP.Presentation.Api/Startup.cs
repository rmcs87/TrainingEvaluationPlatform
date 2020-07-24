using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TEP.Shared;
using TEP.Application;
using TEP.Infra.AuthProvider;
using TEP.Infra.Files;
using TEP.Infra.Persistence;
using System.IO;
using System;
using TEP.Infra.DateTimeService;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TEP.Presentation.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            HostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddFileService();
            services.AddInfraPersistence(Configuration, HostEnvironment);
            services.AddAuthProvider(Configuration);
            services.AddApplication(Configuration);
            services.AddDateTime();

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

            var keyLocation = Path.Combine(Environment.CurrentDirectory, Configuration["keyFileName"]);
            var key = KeyGenerator.Loadkey(keyLocation);

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
            ApplicationDbContextSeed.SeedSampleDataAsync(context);            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
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
