namespace MediAgenda.Application.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameComplete { get; set; }
        public string PhoneNumber { get; set; }
        public DoctorSimpleDTO Doctor { get; set; }
        public PatientSimpleDTO Patient { get; set; }
    }

    public class ApplicationUserSimpleDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameComplete { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ApplicationUserCreateDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NameComplete { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
