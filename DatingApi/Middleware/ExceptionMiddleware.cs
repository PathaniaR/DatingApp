using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this._next=next;
            this._logger=logger;
            this._environment=env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           try
           {
               await _next(context);
           }
           catch (System.Exception ex)
           {
               
               _logger.LogError(ex,ex.Message);
               context.Response.ContentType="application/json";
               context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

               var response = _environment.IsDevelopment()? new ApiException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString()):
                                                            new ApiException(context.Response.StatusCode,"Internal Server Error");
               var options = new  JsonSerializerOptions{PropertyNamingPolicy =JsonNamingPolicy.CamelCase};
               var json=JsonSerializer.Serialize(response,options);
               await context.Response.WriteAsync(json);
           }
        }
    }
}