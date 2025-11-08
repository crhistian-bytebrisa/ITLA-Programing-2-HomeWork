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
    [Table("Prescriptions")]
    public class PrescriptionModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        public ConsultationModel Consultation { get; set; }

        [MaxLength(1000)]
        public string? GeneralRecomendations { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastPrint { get; set; }

        //Navegation
        public List<PrescriptionPermissionModel> PrescriptionPermissions { get; set; }
        public List<PrescriptionMedicineModel> PrescriptionMedicines { get; set; }
        public List<PrescriptionAnalysisModel> PrescriptionAnalysis { get; set; }

        public PrescriptionModel() { }
    }
}
