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
    public class PrescriptionAnalysis
    {
        [Required, ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        [Required, ForeignKey("Analysis")]
        public int AnalysisId { get; set; }
        public Analysis Analysis { get; set; }

        [MaxLength(200),MinLength(10)]
        public string Recomendations { get; set; }
        public PrescriptionAnalysis() { }
    }
}
