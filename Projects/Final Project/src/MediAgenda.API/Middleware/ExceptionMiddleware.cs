
using MediAgenda.Infraestructure.Exceptions;

namespace MediAgenda.API.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                return next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                
                return context.Response.WriteAsJsonAsync(new { Error = "" });

            }
        }
    }
}
