using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models.Relations
{
    [Table("CurrentMedicaments")]
    public class CurrentMedicamentsModel
    {
        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientModel Patient { get; set; }

        [Required, ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public MedicineModel Medicine { get; set; }

        [Required]
        public DateOnly StartMedication { get; set; }
        public DateOnly? EndMedication { get; set; }
    }
}
