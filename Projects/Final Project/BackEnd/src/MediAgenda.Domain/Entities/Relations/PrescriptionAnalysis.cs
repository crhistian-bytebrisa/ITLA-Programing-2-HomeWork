using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities.Relations
{
    public class PrescriptionAnalysis
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public int AnalysisId { get; set; }
        public Analysis Analysis { get; set; }
        public string Recomendations { get; set; }
        public PrescriptionAnalysis() { }
    }
}
