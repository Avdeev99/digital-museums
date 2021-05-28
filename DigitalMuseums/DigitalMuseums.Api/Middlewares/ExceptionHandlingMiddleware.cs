using System;
using System.Net;
using System.Threading.Tasks;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DigitalMuseums.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BaseException exception)
            {
                switch (exception)
                {
                    case BusinessLogicException businessLogicException:
                        var errorResponse = new ErrorResponse(businessLogicException.Message, businessLogicException.StatusCode, businessLogicException.Errors);
                        await WriteToResponse(context, errorResponse);
                        break;
                }
            }
            catch (Exception exception)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                var errorMessage = GetExceptionMessage(exception);

                var errorResponse = new ErrorResponse(errorMessage, statusCode);
                await WriteToResponse(context, errorResponse);
            }
        }
        
        private async Task WriteToResponse(HttpContext ctx, ErrorResponse errorDto)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = errorDto.Status;
            var errorStr = JsonConvert.SerializeObject(errorDto);
            await ctx.Response.WriteAsync(errorStr);
        }
        
        private static string GetExceptionMessage(Exception ex)
        {
            return $"{ ex.Message}{(ex.InnerException == null ? "" : "\n" + GetExceptionMessage(ex.InnerException))}";
        }
    }
}