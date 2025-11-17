using System.Text.Json;

namespace Medinexus.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                success = false,
                message = exception.Message,
                exceptionType = exception.GetType().FullName,
                stackTrace = exception.StackTrace
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
