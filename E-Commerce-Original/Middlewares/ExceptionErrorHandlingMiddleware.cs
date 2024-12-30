using Domain.Exceptions;
using Shared.ErrorBody;
using System.Net;

namespace E_Commerce_Original.Middlewares
{
    public class ExceptionErrorHandlingMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionErrorHandlingMiddleware> logger;

        public ExceptionErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionErrorHandlingMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
                if(httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {

                await handleNotFoundEndPointException(httpContext);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"something wennt wrong! {ex}");
                await handleException(httpContext, ex);
            }

        }

        private async Task handleNotFoundEndPointException(HttpContext httpContext)
        {
           
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = $"The End Point with {httpContext.Request.Path} is not found",
            }.Message.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task handleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
               _ => (int)HttpStatusCode.InternalServerError
            };
            var error = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
            }.ToString();
            await httpContext.Response.WriteAsync(error);
        }
    }
}
