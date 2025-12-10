using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Reasons")]
    public class ReasonModel : IHasTitle, IEntityInt
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public bool Available { get; set; } = true;

        //Navegation
        public List<ConsultationModel> Consultations { get; set; } = new();
        public ReasonModel() { }
    }
}
