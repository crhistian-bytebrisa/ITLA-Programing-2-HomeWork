using System.Diagnostics;

namespace MediAgenda.API.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Request: {method} \nURL: {url} \nQuery: {query}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString.Value
            );

            try
            {
                await _next(context);
            }
            finally
            {
                watch.Stop();
                _logger.LogInformation(
                    "Response: {status} \nTiempo: {elapsed} ms",
                    context.Response.StatusCode,
                    watch.ElapsedMilliseconds
                );
            }
        }
    }


    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
