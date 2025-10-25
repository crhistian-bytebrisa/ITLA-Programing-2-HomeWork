using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Prescriptions")]
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }

        [MaxLength(1000)]
        public string? GeneralRecomendations { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastPrint { get; set; }
        public Prescription() { }
    }
}
