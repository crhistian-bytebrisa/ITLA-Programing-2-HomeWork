using MediAgenda.Application.DTOs.Enums;
using MediAgenda.Application.DTOs.Relations;

namespace MediAgenda.Application.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserSimpleDTO User { get; set; }
        public int InsuranceId { get; set; }
        public InsuranceSimpleDTO Insurance { get; set; }
        public string Identification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int age;
                age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        public string BloodType { get; set; }
        public string Gender { get; set; }
        public NotePatientSimpleDTO NotePatient { get; set; }
        public List<ConsultationSimpleDTO> Consultations { get; set; }
        public List<MedicalDocumentSimpleDTO> MedicalDocuments { get; set; }
        public List<CurrentMedicamentSimpleDTO> CurrentMedicaments { get; set; }
    }

    public class PatientSimpleDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int InsuranceId { get; set; }
        public string Identification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int age;
                age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        public string BloodType { get; set; }
        public string Gender { get; set; }
    }

    public class PatientCreateDTO
    {
        public string UserId { get; set; }
        public int InsuranceId { get; set; }
        public string Identification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodTypeDTO BloodTypeDTO { get; set; }
        public GenderDTO GenderDTO { get; set; }
    }
}
