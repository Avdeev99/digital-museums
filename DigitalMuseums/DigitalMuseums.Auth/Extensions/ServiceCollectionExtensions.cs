using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Auth.Factories;
using DigitalMuseums.Auth.Factories.Interfaces;
using DigitalMuseums.Auth.Options;
using DigitalMuseums.Auth.Tokens;
using DigitalMuseums.Auth.Tokens.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMuseums.Auth.Extensions
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
        /// <param name="configuration">Configuration.</param>
        /// <returns>An instance of <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleAuthOptions>(configuration.GetSection(nameof(GoogleAuthOptions)));
            services.AddScoped<ITokenFactory, JwtTokenFactory>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            return services;
        }
    }
}