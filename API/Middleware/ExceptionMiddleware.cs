using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {   //requestDelegate=>what's next in the middleware pipeline/ILogger=>to log out our exception in terminal
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message); //if we don't do that, the exception is not going to appear in the terminal
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError; //500 error to the error response

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, e.Message, e.StackTrace?.ToString()) //if we are in development mode
                    : new ApiException(context.Response.StatusCode, "Interal Server Error"); //if we are in production mode

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; //this is to ensure that the response
                                                                                    //goes back as a normal Json formatted response in CamelCase
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);                
            }
        }
    }
}