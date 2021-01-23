using DigitalMuseums.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DigitalMuseums.Api.Extensions
{
    /// <summary>
    /// The middleware extensions.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// The use exception handling middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}