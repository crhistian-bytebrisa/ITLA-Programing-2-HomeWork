using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Insurance
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Map
        public List<Patient> Patients { get; set; } = new List<Patient>();

        public Insurance()
        {

        }
    }
}
