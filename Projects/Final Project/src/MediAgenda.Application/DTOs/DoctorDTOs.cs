namespace MediAgenda.Application.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserSimpleDTO User { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }

    public class DoctorSimpleDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }

    public class DoctorCreateDTO
    {
        public string UserId { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }
    }
}
