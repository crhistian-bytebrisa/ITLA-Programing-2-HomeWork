using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("NotesConsultations")]
    public class NoteConsultationModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        public ConsultationModel Consultation { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdateAt { get; set; }


        public NoteConsultationModel() { }
    }
}
