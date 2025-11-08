using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Doctors")]
    public class DoctorModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required, MaxLength(200)]
        public string Specialty { get; set; }

        [MaxLength(500)]
        public string AboutMe { get; set; }

        public DoctorModel()
        {

        }
    }

}
