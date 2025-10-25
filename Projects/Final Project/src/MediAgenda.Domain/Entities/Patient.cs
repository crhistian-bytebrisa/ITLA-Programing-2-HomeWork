using MediAgenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
        public string Identification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Bloodtype Bloodtype { get; set; }

        //Map
        public NotePatient NotePatient { get; set; } = new();

        public Patient() { }

    }

}
