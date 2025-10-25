using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Clinics")]
    public class Clinic
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(300), MinLength(20)]
        public string Address { get; set; }

        [Required, StringLength(11)]
        public string PhoneNumber { get; set; }

        //Map
        public List<DayAvailable> DaysAvailable { get; set; } = new();
        public Clinic() { }
    }
}
