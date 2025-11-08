using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Insurance : Entity
    {
        public string Name { get; set; }

        //Navegation
        public List<Patient> Patients { get; set; } = new List<Patient>();

        public Insurance()
        {

        }
    }
}
