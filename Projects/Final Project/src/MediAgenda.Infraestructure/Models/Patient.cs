using MediAgenda.Infraestructure.Models.Enums;
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
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required, ForeignKey("Insurance")]
        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }

        [Required]
        public string Identification { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Bloodtype Bloodtype { get; set; }

        //Map
        public NotePatient NotePatient { get; set; } = new();
        public List<Consultation> Consultations { get; set; } = new();

        public Patient() { }

    }

}
