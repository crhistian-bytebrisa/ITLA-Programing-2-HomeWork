using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("DaysAvailable")]
    public class DayAvailable
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Limit { get; set; } = 10;

        //Map
        public List<Consultation> Consultations { get; set; } = new();

        public DayAvailable()
        {

        }
       
    }
}
