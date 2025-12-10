using MediAgenda.Domain.Core;
using MediAgenda.Domain.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Analysis : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Navegation
        public List<PrescriptionAnalysis> PrescriptionAnalysis { get; set; }
        public Analysis() { }
    }
}
