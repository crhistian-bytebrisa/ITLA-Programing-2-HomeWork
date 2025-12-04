using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities.Relations
{
    public class CurrentMedicaments
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public DateOnly StartMedication { get; set; }
        public DateOnly? EndMedication { get; set; }
    }
}
