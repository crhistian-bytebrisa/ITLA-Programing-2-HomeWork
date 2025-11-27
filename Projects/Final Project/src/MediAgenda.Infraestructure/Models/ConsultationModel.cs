using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Interfaces;
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
    public class ConsultationModel : IEntityInt
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientModel Patient { get; set; }

        [Required, ForeignKey("Reason")]
        public int ReasonId { get; set; }
        public ReasonModel Reason { get; set; }

        [Required, ForeignKey("DayAvailable")]
        public int DayAvailableId { get; set; }
        public DayAvailableModel DayAvailable { get; set; }

        [Required]
        public ConsultationState State { get; set; } = ConsultationState.Pendent;

        [Required]
        public int Turn { get; set; }

        //Navegation
        public NoteConsultationModel Note { get; set; } = new();
        public List<PrescriptionModel> Prescriptions { get; set; } = new();

        public ConsultationModel() { }
    }
}
