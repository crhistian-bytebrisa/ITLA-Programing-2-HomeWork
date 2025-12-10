using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MediAgenda.API.Filters
{
    public class AuthorizeSameUserOrRoles : AuthorizeAttribute, IAuthorizationFilter
    {
        public string _userId { get; set; }
        private string[] _allowedRoles { get; set; }

        // se envia "id", "rol1", "rol2", etc.
        /// <summary>
        /// Esta autorizacion se encarga de verificar si el usuario tiene el mismo Id, o si tiene uno de los roles permitidos.
        /// </summary>
        /// <param name="userIdParameter">Nombre del parametro en la peticion que contenga el Id del usuario.</param>
        /// <param name="allowedRoles">Los roles que tengan acceso al endpoint.</param>
        /// <example>
        /// [AuthorizeSameUserOrRoles("id", "Admin", "Manager")]
        /// </example>
        public AuthorizeSameUserOrRoles(string userIdParameter = "userId", params string[] allowedRoles)
        {
            _userId = userIdParameter;
            _allowedRoles = allowedRoles;
        }

        //Funcion que ejecuta la autorizacion
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Si tiene un rol permitido, autorizar
            if (_allowedRoles != null && _allowedRoles.Length > 0)
            {
                if (_allowedRoles.Any(role => user.IsInRole(role)))
                {
                    return;
                }
            }

            // Si no tiene el rol, verificar que sea el mismo usuario
            var currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var routeUserId = context.RouteData.Values[_userId]?.ToString();

            //Si no tiene ninguno, que se fuña
            if (string.IsNullOrEmpty(currentUserId) || currentUserId != routeUserId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
