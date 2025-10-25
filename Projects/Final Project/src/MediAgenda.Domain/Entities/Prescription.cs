using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }
        public Prescription() { }
    }
}
