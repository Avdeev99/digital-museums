using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMuseums.Core.Extensions
{
    /// <summary>
    /// Represents extensions for <see cref="IServiceCollection" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for accessing the Database.
        /// </summary>
        /// <param name="services">Services collection.</param>
        /// <returns>An instance of <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IMuseumService, MuseumService>();
            
            services.AddScoped<IMuseumFilterPipeline, MuseumFilterPipeline>();

            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinaryOptions>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}