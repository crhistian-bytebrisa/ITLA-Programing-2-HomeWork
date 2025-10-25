using MediAgenda.Infraestructure.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Consultations")]
    public class Consultation
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required, ForeignKey("Reason")]
        public int ReasonId { get; set; }
        public Reason Reason { get; set; }

        [Required, ForeignKey("DayAvailable")]
        public int DayAvailableId { get; set; }
        public DayAvailable DayAvailable { get; set; }

        [Required]
        public ConsultationState State { get; set; } = ConsultationState.Pendent;

        [Required]
        public int Turn { get; set; }

        public Consultation() { }
    }
}
