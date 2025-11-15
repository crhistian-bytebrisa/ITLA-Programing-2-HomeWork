using MediAgenda.API.DTOs.Enums;

namespace MediAgenda.API.DTOs
{
    public class ConsultationDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public int ReasonId { get; set; }
        public ReasonSimpleDTO Reason { get; set; }
        public int DayAvailableId { get; set; }
        public DayAvailableSimpleDTO DayAvailable { get; set; }
        public ConsultationStateDTO State { get; set; }
        public int Turn { get; set; }
        public NoteConsultationSimpleDTO Note { get; set; }
        public List<PrescriptionSimpleDTO> Prescriptions { get; set; }
    }

    public class ConsultationSimpleDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ReasonId { get; set; }
        public int DayAvailableId { get; set; }
        public ConsultationStateDTO State { get; set; }
        public int Turn { get; set; }
    }

    public class ConsultationCreateDTO
    {
        public int PatientId { get; set; }
        public int ReasonId { get; set; }
        public int DayAvailableId { get; set; }
        public ConsultationStateDTO State { get; set; }
    }
}
