namespace MediAgenda.Application.DTOs
{
    public class InsuranceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PatientSimpleDTO> Patients { get; set; }
    }

    public class InsuranceSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InsuranceCreateDTO
    {
        public string Name { get; set; }
    }
}
