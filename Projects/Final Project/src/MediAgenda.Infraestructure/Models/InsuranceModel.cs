using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Insurances")]
    public class InsuranceModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(5)]
        public string Name { get; set; }

        //Navegation
        public List<PatientModel> Patients { get; set; } = new List<PatientModel>();

        public InsuranceModel()
        {

        }
    }
}
