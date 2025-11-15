namespace MediAgenda.Application.DTOs
{
    public class NoteConsultationDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public ConsultationSimpleDTO Consultation { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NoteConsultationSimpleDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NoteConsultationCreateDTO
    {
        public int ConsultationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
