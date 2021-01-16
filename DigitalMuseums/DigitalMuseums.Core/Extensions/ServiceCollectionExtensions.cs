using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Core.Services;
using DigitalMuseums.Core.Services.Contracts;
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

            return services;
        }
    }
}