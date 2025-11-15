namespace MediAgenda.Application.DTOs.Relations
{
    public class PrescriptionMedicineDTO
    {
        public int PrescriptionId { get; set; }
        public PrescriptionSimpleDTO Prescription { get; set; }
        public int MedicineId { get; set; }
        public MedicineSimpleDTO Medicine { get; set; }
        public string Instructions { get; set; }
    }

    public class PrescriptionMedicineSimpleDTO
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineFormat { get; set; }
        public string Instructions { get; set; }
    }

    public class PrescriptionMedicineCreateDTO
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string Instructions { get; set; }
    }
}
