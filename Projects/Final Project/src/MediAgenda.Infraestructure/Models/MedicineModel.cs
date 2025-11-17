using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Medicines")]
    public class MedicineModel : IHasName
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required, MaxLength(50)]
        public string Format { get; set; }

        //Navegation
        public List<PrescriptionMedicineModel> PrescriptionMedicines { get; set; }
        public List<CurrentMedicamentsModel> CurrentMedicaments { get; set; } = new();

        public MedicineModel() { }
    }
}
