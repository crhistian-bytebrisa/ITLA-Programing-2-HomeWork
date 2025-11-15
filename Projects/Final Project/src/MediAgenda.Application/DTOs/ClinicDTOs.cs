namespace MediAgenda.Application.DTOs
{
    public class ClinicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<DayAvailableSimpleDTO> DaysAvailable { get; set; }
    }

    public class ClinicSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ClinicCreateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
