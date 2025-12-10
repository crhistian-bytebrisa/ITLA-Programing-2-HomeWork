using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class PrescriptionRequest : BaseRequest
    {
        public int? PatientId { get; set; } 
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? LastPrintFrom { get; set; }
        public DateTime? LastPrintTo { get; set; }
        public bool? IncludeConsultation { get; set; } 
    }
}
