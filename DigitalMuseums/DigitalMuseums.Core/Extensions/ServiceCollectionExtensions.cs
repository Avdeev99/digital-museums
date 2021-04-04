using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.DTO.Museum;
using DigitalMuseums.Core.Domain.Models.Domain;
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
            services.AddScoped<IExhibitService, ExhibitService>();
            services.AddScoped(typeof(IBasePredefinedEntityService<>), typeof(BasePredefinedEntityService<>));

            services.AddScoped<IOrderedFilterPipeline<Museum, FilterMuseumsDto>, MuseumFilterPipeline>();
            services.AddScoped<IFilterPipeline<Exhibit, FilterExhibitsDto>, ExhibitFilterPipeline>();

            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinaryOptions>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}