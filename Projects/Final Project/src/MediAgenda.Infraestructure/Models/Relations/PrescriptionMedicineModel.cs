using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models.Relations
{
    [Table("PrescriptionsMedicines")]
    public class PrescriptionMedicineModel
    {
        [Required, ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public PrescriptionModel Prescription { get; set; }

        
        [Required, ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public MedicineModel Medicine { get; set; }


        [Required, MaxLength(300), MinLength(10)]
        public string Instructions { get; set; }

        public PrescriptionMedicineModel() { }
    }
}
