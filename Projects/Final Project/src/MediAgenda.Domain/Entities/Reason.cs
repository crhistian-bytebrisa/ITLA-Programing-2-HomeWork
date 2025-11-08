using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Reason : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; } = true;

        //Navegation
        public List<Consultation> Consultations { get; set; } = new();
        public Reason() { }
    }
}
