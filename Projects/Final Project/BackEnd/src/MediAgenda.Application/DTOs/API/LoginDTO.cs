using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.DTOs.API
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electronico valido.")]
        public string Email { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
