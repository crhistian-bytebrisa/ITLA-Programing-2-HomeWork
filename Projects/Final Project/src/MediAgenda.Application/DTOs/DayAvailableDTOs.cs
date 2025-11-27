using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class DayAvailableDTO
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public ClinicDTO Clinic { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public int Limit { get; set; }
        public List<ConsultationSimpleDTO> Consultations { get; set; }
        public int AvailableSlots => Limit - (Consultations?.Count ?? 0);
        public bool IsAvailable => AvailableSlots > 0;
    }

    public class DayAvailableSimpleDTO
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public int Limit { get; set; }
        public int ConsultationsCount { get; set; }
        public int AvailableSlots => Limit - ConsultationsCount;
        public bool IsAvailable => AvailableSlots > 0;
    }

    public class DayAvailableCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int ClinicId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(typeof(TimeOnly), "00:00", "23:59", ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(5,50)]
        public int Limit { get; set; }
    }

    public class DayAvailableUpdateDTO : DayAvailableCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }
    }
}
