using MediAgenda.Domain.Core;
using MediAgenda.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Medicine : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }


        //Navegation
        public List<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public List<CurrentMedicaments> CurrentMedicaments { get; set; }
        public Medicine() { }
    }
}
