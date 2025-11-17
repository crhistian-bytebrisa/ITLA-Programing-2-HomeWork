using Azure.Core;
using System.Diagnostics;

namespace MediAgenda.API.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public void SaveLogin(string message)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string fileName = $"Logging-{date}.txt";
                string directory = "logs";
                string fullPath = Path.Combine(directory, fileName);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string logEntry = $"[{timestamp}] {message}{Environment.NewLine}";

                File.AppendAllText(fullPath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando log: {ex.Message}");
            }
        }

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            string message = $"Request: {context.Request.Method} \nURL: {context.Request.Path} \nQuery: {context.Request.QueryString.Value}";

            _logger.LogInformation(message);

            try
            {
                await _next(context);
            }
            finally
            {
                watch.Stop();

                string mes = $"Response: {context.Response.StatusCode} \nTiempo: {watch.ElapsedMilliseconds} ms";
                _logger.LogInformation(mes);

                message += mes;

                SaveLogin(message+"\n\n");

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
