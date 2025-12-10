using MediAgenda.Application.DTOs.Enums;
using MediAgenda.Application.DTOs.Relations;
using System.ComponentModel.DataAnnotations;

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
        public int NotesCount { get; set; }
        public int ConsultationsCount { get; set; }
        public int MedicalDocumentsCount { get; set; }
        public int CurrentMedicamentsCount { get; set; }
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
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public BloodTypeDTO BloodTypeDTO { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public GenderDTO GenderDTO { get; set; }
    }

    public class PatientUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public BloodTypeDTO BloodTypeDTO { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public GenderDTO GenderDTO { get; set; }
    }
    
}