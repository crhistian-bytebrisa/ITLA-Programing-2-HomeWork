namespace MediAgenda.Application.DTOs
{
    public class ReasonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public List<ConsultationSimpleDTO> Consultations { get; set; }
    }

    public class ReasonSimpleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }

    public class ReasonCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }
}
