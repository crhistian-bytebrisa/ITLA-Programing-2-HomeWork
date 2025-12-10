using MediAgenda.Application.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.DTOs.API
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electronico valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        [MaxLength(200, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        [MinLength(3, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string NameComplete { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Phone(ErrorMessage = "El campo {0} debe ser un numero de telefono valido.")]
        [MinLength(10, ErrorMessage = "Debes tener al menos {1} caracteres en el {0}.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public BloodTypeDTO BloodTypeDTO { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public GenderDTO GenderDTO { get; set; }

    }
}
