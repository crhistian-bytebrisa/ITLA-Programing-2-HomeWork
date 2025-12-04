using MediAgenda.Domain.Core;
using MediAgenda.Domain.Entities.Relations;
using MediAgenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Patient : Entity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
        public string Identification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Bloodtype Bloodtype { get; set; }
        public string Gender { get; set; }

        //Navegation
        public NotePatient NotePatient { get; set; } = new();
        public List<Consultation> Consultations { get; set; } = new();
        public List<MedicalDocument> MedicalDocuments { get; set; } = new();
        public List<CurrentMedicaments> CurrentMedicaments { get; set; } = new();

        public Patient() { }

    }

}
