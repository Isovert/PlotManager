using PlotManager.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace PlotManager.API.Middleware.ExceptionHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
                                      ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                string errorMessage = string.Empty;
                response.ContentType = "application/json";
                switch (error)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _logger.LogError($"Error in request path: {context.Request.Path}. Error message: {error.Message}");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorMessage = JsonSerializer.Serialize(new { message = "Internal server error." });
                        break;
                }
                await response.WriteAsync(errorMessage);
            }
        }
    }
}