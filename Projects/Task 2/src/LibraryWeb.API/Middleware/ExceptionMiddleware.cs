using LibraryWeb.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace GestionTareas.API.Middleware
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

        public async Task Invoke(HttpContext ctx)
        {
            try
            { 
                await _next(ctx);
            }
            catch(DomainException ex)
            {
                ctx.Response.StatusCode = 404;
                _logger.LogWarning(ex,"Error de dominio");
                await ctx.Response.WriteAsJsonAsync(new EntityMiddleware(false,"Error de dominio: " + ex.Message,404));
            }
            catch (InfraestructureException ex)
            {
                ctx.Response.StatusCode = 404;
                _logger.LogWarning(ex, "Error de infraestructura");
                await ctx.Response.WriteAsJsonAsync(new EntityMiddleware(false, "Error de infraestructura: " + ex.Message, 404));
            }
            catch (AppException ex)
            {
                ctx.Response.StatusCode = 404;
                _logger.LogWarning(ex, "Error de aplicacion");
                await ctx.Response.WriteAsJsonAsync(new EntityMiddleware(false, "Error de Applicacion: " + ex.Message, 404));
            }
            catch (APIException ex)
            {
                ctx.Response.StatusCode = 409;
                _logger.LogWarning(ex, "Error de la API");
                await ctx.Response.WriteAsJsonAsync(new EntityMiddleware(false, "Error de la API: " + ex.Message, 409));
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                _logger.LogWarning(ex, "Error inesperado");
                await ctx.Response.WriteAsJsonAsync(new EntityMiddleware(false,"Error inesperado: " + ex.Message, 500));
            }
        }
    }
}
