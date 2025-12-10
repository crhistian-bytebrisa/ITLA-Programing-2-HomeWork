using MediAgenda.Infraestructure.Models.Enums;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class ConsultationRequest : BaseRequest
    {
        public int? PatientId { get; set; }
        public int? ReasonId { get; set; }
        public int? DayAvailableId { get; set; }
        public ConsultationState? State { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public bool? IncludeNote { get; set; }
        public bool? IncludePrescription { get; set; }
        public bool? IncludePatient { get; set; }
        public bool? IncludeReason { get; set; }
        public bool? IncludeDayAvailable { get; set; }
    }

    public class ConsultationNoteRequest : BaseRequest
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public string? Title { get; set; }
    }
}
