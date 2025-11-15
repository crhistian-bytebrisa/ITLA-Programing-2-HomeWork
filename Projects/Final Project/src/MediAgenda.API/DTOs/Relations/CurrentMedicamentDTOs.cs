namespace MediAgenda.API.DTOs.Relations
{
    public class CurrentMedicamentDTO
    {
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public int MedicineId { get; set; }
        public MedicineSimpleDTO Medicine { get; set; }
        public DateOnly StartMedication { get; set; }
        public DateOnly? EndMedication { get; set; }
        public bool IsActive => EndMedication == null || EndMedication >= DateOnly.FromDateTime(DateTime.Today);
    }

    public class CurrentMedicamentSimpleDTO
    {
        public int PatientId { get; set; }
        public int MedicineId { get; set; }
        public DateOnly StartMedication { get; set; }
        public DateOnly? EndMedication { get; set; }
        public bool IsActive => EndMedication == null || EndMedication >= DateOnly.FromDateTime(DateTime.Today);
    }

    public class CurrentMedicamentCreateDTO
    {
        public int PatientId { get; set; }
        public int MedicineId { get; set; }
        public DateOnly StartMedication { get; set; }
        public DateOnly? EndMedication { get; set; }
    }
}
