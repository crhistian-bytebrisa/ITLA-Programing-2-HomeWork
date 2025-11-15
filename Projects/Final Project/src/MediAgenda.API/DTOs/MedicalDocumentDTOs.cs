namespace MediAgenda.API.DTOs
{
    public class MedicalDocumentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
    }

    public class MedicalDocumentSimpleDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
    }

    public class MedicalDocumentCreateDTO
    {
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
    }
}
