using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserSimpleDTO User { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }

    public class DoctorSimpleDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }

    public class DoctorCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(200, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }

    public class DoctorUpdateDTO : DoctorCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }
        
    }
}
