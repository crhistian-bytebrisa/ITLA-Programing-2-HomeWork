using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MediAgenda.API.Filters
{
    public class AuthorizeSamePatientIdOrRoles : AuthorizeAttribute, IAuthorizationFilter
    {
        public string _patientId { get; set; }
        private string[] _allowedRoles { get; set; }

        // se envia "id", "rol1", "rol2", etc.
        /// <summary>
        /// Esta autorizacion se encarga de verificar si el usuario contiene el paciente y tiene el mismo Id, o si tiene uno de los roles permitidos.
        /// </summary>
        /// <param name="patientIdParameter">Nombre del parametro en la peticion que contenga el Id del paciente.</param>
        /// <param name="allowedRoles">Los roles que tengan acceso al endpoint.</param>
        /// <example>
        /// [AuthorizeSameUserOrRoles("patientId", "Admin", "Manager")]
        /// </example>
        public AuthorizeSamePatientIdOrRoles(string patientIdParameter = "patientId", params string[] allowedRoles)
        {
            _patientId = patientIdParameter;
            _allowedRoles = allowedRoles;
        }

        //Funcion que ejecuta la autorizacion
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var patient = context.HttpContext.User;

            // Si tiene un rol permitido, autorizar
            if (_allowedRoles != null && _allowedRoles.Length > 0)
            {
                if (_allowedRoles.Any(role => patient.IsInRole(role)))
                {
                    return;
                }
            }

            // Si no tiene el rol, verificar que sea el mismo usuario
            var currentUserId = patient.FindFirst("PatientId")?.Value;
            var routeUserId = context.RouteData.Values[_patientId]?.ToString();

            //Si no tiene ninguno, que se fuña
            if (string.IsNullOrEmpty(currentUserId) || currentUserId != routeUserId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
