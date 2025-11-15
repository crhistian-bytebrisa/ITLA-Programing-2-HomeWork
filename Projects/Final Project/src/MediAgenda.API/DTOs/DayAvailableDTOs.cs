namespace MediAgenda.API.DTOs
{
    public class DayAvailableDTO
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public ClinicSimpleDTO Clinic { get; set; }
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
        public int ClinicId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public int Limit { get; set; }
    }
}
