using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using DigitalMuseums.Api.Extensions;
using DigitalMuseums.Api.Filters;
using DigitalMuseums.Api.Mappings;
using DigitalMuseums.Auth.Extensions;
using DigitalMuseums.Core.Extensions;
using DigitalMuseums.Core.Mappings;
using DigitalMuseums.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Stripe;

namespace DigitalMuseums.Api
{
    public class Startup
    {
        private const string DatabaseConnectionStringName = "DigitalMuseumsDb";

        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> reference.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers required services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> reference.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = configuration.GetConnectionString(DatabaseConnectionStringName);
            services.AddDbDataAccess(dbConnectionString);
            services.AddCore(configuration);
            services.AddAuth(configuration);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ApiMappingProfile>();
                cfg.AddProfile<ImageMappingProfile>();
                cfg.AddProfile<MuseumMappingProfile>();
                cfg.AddProfile<SouvenirMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<ExhibitMappingProfile>();
                cfg.AddProfile<ExhibitionMappingProfile>();
                cfg.AddProfile<CartMappingProfile>();
            });
            services.AddApi();
            
            services.AddHttpContextAccessor();

            AuthConfiguratorExtensions.Configure(services, configuration);
            services.AddCloudinary(configuration);

            services.AddSwaggerGen(
                setup =>
                {
                    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalMuseums.API", Version = "v1" });

                    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Please add Bearer prefix before {token}",
                        Name = HeaderNames.Authorization,
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "Bearer",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });

                    setup.CustomSchemaIds(x => x.FullName);
                });

            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ModelValidityFilter));
                })
                .ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericPredefinedEntityControllerProvider()))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalMuseums.API v1");
            });

            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseCors(corsPolicy =>
                {
                    corsPolicy.AllowAnyHeader();
                    corsPolicy.AllowAnyMethod();
                    corsPolicy.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseCors(corsPolicy =>
                {
                    corsPolicy.AllowAnyHeader();
                    corsPolicy.AllowAnyMethod();
                    corsPolicy.AllowAnyOrigin();
                });
                app.UseHsts();
            }

            app.UseExceptionHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();

            AuthConfiguratorExtensions.Configure(app);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}