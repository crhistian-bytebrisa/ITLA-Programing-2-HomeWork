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
    }

    public class PatientConsultationRequest : BaseRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public int? ReasonId { get; set; }
        public int? ClinicId { get; set; }
        public ConsultationState? State { get; set; }
    }

    public class PatientMedicalDocumentRequest : BaseRequest
    {
        public string? DocumentType { get; set; }
    }

    public class PatientNoteRequest : BaseRequest
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public string? Title { get; set; }
    }

    public class PatientMedicamentRequest : BaseRequest
    {
        public bool? IsCurrent { get; set; }
        public string? MedicamentName { get; set; }
    }
}
