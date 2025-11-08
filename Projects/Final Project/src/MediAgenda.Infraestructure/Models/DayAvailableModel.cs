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
    public class DayAvailableModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public ClinicModel Clinic { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int Limit { get; set; } = 10;

        //Navegation
        public List<ConsultationModel> Consultations { get; set; } = new();

        public DayAvailableModel()
        {

        }
       
    }
}
