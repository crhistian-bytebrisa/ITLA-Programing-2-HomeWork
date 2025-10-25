using MediAgenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Consultation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int ReasonId { get; set; }
        public Reason Reason { get; set; }
        public int DayAvailableId { get; set; }
        public DayAvailable DayAvailable { get; set; }
        public ConsultationState State { get; set; }
        public int Turn { get; set; }

        public Consultation() { }
    }
}
