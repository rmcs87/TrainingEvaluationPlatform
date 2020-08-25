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
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Training Evaluation Platform",
                    Version = "v1",
                    Description = "TEP is a open-source project written in .NET Core for BackEnding VR Trainning Aplications.",
                    TermsOfService = new Uri("https://github.com/rmcs87/TrainingEvaluationPlatform"),
                    Contact = new OpenApiContact
                    {
                        Name = "LAW-VR",
                        Email = "ricardo.costa@uvv.br",
                        Url = new Uri("https://github.com/rmcs87/TrainingEvaluationPlatform"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/rmcs87/TrainingEvaluationPlatform"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpContextAccessor();

            services.AddFileService();
            services.AddInfraPersistence(Configuration);
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
            app.UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integrating Swagger");
            });

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

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
