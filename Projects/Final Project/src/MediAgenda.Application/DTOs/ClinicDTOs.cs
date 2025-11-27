using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class ClinicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DaysAvailableCount { get; set; }
    }

    public class ClinicCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        [MinLength(20, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(300, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        [MinLength(20, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Phone(ErrorMessage = "El campo {0} debe ser un numero de telefono valido.")]
        [MinLength(10, ErrorMessage = "Debes tener al menos {1} caracteres en {0}.")]
        public string PhoneNumber { get; set; }
    }

    public class ClinicUpdateDTO : ClinicCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }
    }
}
