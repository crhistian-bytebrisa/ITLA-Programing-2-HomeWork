using MediAgenda.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameComplete { get; set; }
        public string PhoneNumber { get; set; }
        public DoctorSimpleDTO Doctor { get; set; }
        public PatientSimpleDTO Patient { get; set; }
    }

    public class ApplicationUserSimpleDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameComplete { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ApplicationUserCreateDTO : IHasUsername
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        [MinLength(3, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electronico valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        [MaxLength(200, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        [MinLength(3, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string NameComplete { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Phone(ErrorMessage = "El campo {0} debe ser un numero de telefono valido.")]
        [MinLength(10, ErrorMessage = "Debes tener al menos {1} caracteres en el {0}.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Password { get; set; }

    }

    public class ApplicationUserUpdateDTO : ApplicationUserCreateDTO,IHasUsername 
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Guid Id { get; set; }
    }
}
