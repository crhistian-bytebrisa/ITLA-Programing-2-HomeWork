namespace MediAgenda.Application.DTOs
{
    public class NotePatientDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NotePatientSimpleDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NotePatientCreateDTO
    {
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
