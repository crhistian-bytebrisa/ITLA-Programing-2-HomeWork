using MediAgenda.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class InsuranceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PatientSimpleDTO> Patients { get; set; }
    }

    public class InsuranceSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InsuranceCreateDTO 
    {
        [Required(ErrorMessage = "El campo de Name es requerido.")]

        public string Name { get; set; }
    }
}
