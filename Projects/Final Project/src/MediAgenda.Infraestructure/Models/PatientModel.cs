using MediAgenda.Infraestructure.Models.Enums;
using MediAgenda.Infraestructure.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Patients")]
    public class PatientModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUserModel User { get; set; }

        [Required, ForeignKey("Insurance")]
        public int InsuranceId { get; set; }
        public InsuranceModel Insurance { get; set; }

        [Required]
        public string Identification { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Bloodtype Bloodtype { get; set; }

        [Required]
        public Gender Gender { get; set; }

        //Navegation
        public NotePatientModel? NotePatient { get; set; } 
        public List<ConsultationModel>? Consultations { get; set; } 
        public List<MedicalDocumentModel>? MedicalDocuments { get; set; } 
        public List<CurrentMedicamentsModel>? CurrentMedicaments { get; set; } 

        public PatientModel() { }

    }

}
