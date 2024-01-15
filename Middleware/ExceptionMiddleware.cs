using AutoMapper;
using QuestionsAskingServer.Dtos;
using QuestionsAskingServer.Exceptions;
using System.Net;

namespace QuestionsAskingServer.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogException(context, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogException(HttpContext context, Exception exception)
        {
            var requestPath = context.Request.Path;
            var logMessage = $"Error occurred processing request: {requestPath} - Exception: {exception}";
            _logger.LogError(logMessage);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var massage = exception.Message;

            context.Response.StatusCode = exception switch
            {

                EntityNotFoundException => StatusCodes.Status404NotFound,
                InvalidInputException => StatusCodes.Status400BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            return context.Response.WriteAsync(new ErrorDetailsDto
            {
                StatusCode = context.Response.StatusCode,
                Message = massage
            }.ToString());
        }
    }
}
