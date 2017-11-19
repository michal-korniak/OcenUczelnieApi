using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OcenUczelnie.Core.Domain.Exceptions;

namespace OcenUczelnie.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status;
            if (typeof(Exception) == typeof(UnauthorizedAccessException))
                status = (int) HttpStatusCode.Unauthorized;
            else
                status = (int) HttpStatusCode.InternalServerError;

            var code = "error";
            if (exception is OcenUczelnieException uczelnieException)
                code = uczelnieException.Code;
            var result = JsonConvert.SerializeObject(new
            {
                code=code,
                message = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(result);
        }
    }
}