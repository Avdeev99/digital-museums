using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Domain.DTO.Museum;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline;
using DigitalMuseums.Core.Services;
using DigitalMuseums.Core.Services.Contracts;
using DigitalMuseums.Core.Services.Contracts.Providers;
using DigitalMuseums.Core.Services.Providers;
using DigitalMuseums.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISouvenirService, SouvenirService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ChargeService>();
            
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExhibitionService, ExhibitionService>();
            services.AddScoped(typeof(IBasePredefinedEntityService<>), typeof(BasePredefinedEntityService<>));

            services.AddScoped<IOrderedFilterPipeline<Museum, FilterMuseumsDto>, MuseumFilterPipeline>();
            services.AddScoped<IOrderedFilterPipeline<Souvenir, FilterSouvenirsDto>, SouvenirFilterPipeline>();
            services.AddScoped<IFilterPipeline<Exhibit, FilterExhibitsDto>, ExhibitFilterPipeline>();
            services.AddScoped<IFilterPipeline<Exhibition, FilterExhibitionsDto>, ExhibitionFilterPipeline>();

            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinaryOptions>(configuration.GetSection("CloudinarySettings"));

            return services;
        }
    }
}