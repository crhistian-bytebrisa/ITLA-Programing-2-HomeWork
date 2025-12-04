using MediAgenda.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class InsuranceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PatientsCount { get; set; }
    }

    public class InsuranceSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InsuranceCreateDTO 
    {
        [Required(ErrorMessage = "El campo de Name es requerido.")]
        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en Name.")]
        [MinLength(6, ErrorMessage = "Debes tener al menos {1} caracteres en Name.")]
        public string Name { get; set; }
    }

    public class InsuranceUpdateDTO : InsuranceCreateDTO
    {
        [Required(ErrorMessage = "El campo de Id es requerido.")]
        public int Id { get; set; }
    }
}
