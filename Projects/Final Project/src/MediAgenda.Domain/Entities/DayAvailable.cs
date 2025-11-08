using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class DayAvailable : Entity
    {
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public int Limit { get; set; }

        //Navegation
        public List<Consultation> Consultations { get; set; } = new();

        public DayAvailable()
        {

        }
       
    }
}
