using MediAgenda.Domain.Core;
using MediAgenda.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Prescription : Entity
    {
        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }

        //Navegation
        public List<PrescriptionPermission> PrescriptionPermissions { get; set; }
        public List<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public List<PrescriptionAnalysis> PrescriptionAnalysis { get; set; }
        public Prescription() { }
    }
}
