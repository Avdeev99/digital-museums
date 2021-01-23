using System.Diagnostics.CodeAnalysis;
using DigitalMuseums.Api.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMuseums.Api.Extensions
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
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}