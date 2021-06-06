using DigitalMuseums.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DigitalMuseums.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}