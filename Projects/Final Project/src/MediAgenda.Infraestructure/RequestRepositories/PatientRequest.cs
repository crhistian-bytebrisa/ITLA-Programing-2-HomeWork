using MediAgenda.Infraestructure.Models.Enums;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class PatientRequest : BaseRequest
    {
        public string? Name { get; set; }
        public int? OlderAge { get; set; }
        public int? InsuranceId { get; set; }
        public string? Identification { get; set; } 
        public Bloodtype? Bloodtype { get; set; }
        public Gender? Gender { get; set; }
        public bool? IncludeNote { get; set; }
        public bool? IncludeMedicalDocuments { get; set; }
        public bool? IncludeCurrentMedicaments { get; set; }
        public bool? IncludeConsultations { get; set; } 
        public bool? IncludeUser { get; set; } 
        public bool? IncludeInsurance { get; set; } 
    }
}
