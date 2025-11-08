using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Clinic : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        //Navegation
        public List<DayAvailable> DaysAvailable { get; set; } = new();

        public Clinic() { }
    }
}
