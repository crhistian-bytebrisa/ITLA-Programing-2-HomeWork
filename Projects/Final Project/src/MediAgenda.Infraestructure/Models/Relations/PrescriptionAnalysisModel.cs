using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models.Relations
{
    [Table("PrescriptionsAnalysis")]
    public class PrescriptionAnalysisModel
    {
        [Required, ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public PrescriptionModel Prescription { get; set; }

        [Required, ForeignKey("Analysis")]
        public int AnalysisId { get; set; }
        public AnalysisModel Analysis { get; set; }

        [MaxLength(200),MinLength(10)]
        public string Recomendations { get; set; }
        public PrescriptionAnalysisModel() { }
    }
}
