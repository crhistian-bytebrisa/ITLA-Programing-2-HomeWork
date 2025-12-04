using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class CurrentMedicamentRequest
    {
        public int? PatientId { get; set; }
        public int? MedicineId { get; set; }
        public DateOnly? StartMedicationFrom { get; set; }
        public DateOnly? StartMedicationTo { get; set; }
        public bool? IsActive { get; set; } 
        public bool? IncludePatient { get; set; }
        public bool? IncludeMedicine { get; set; }
    }
}
