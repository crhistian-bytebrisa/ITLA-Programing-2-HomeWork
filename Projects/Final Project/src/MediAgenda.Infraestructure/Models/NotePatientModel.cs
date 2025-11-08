using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("NotesPatients")]
    public class NotePatientModel
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientModel Patient { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(2000)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdateAt { get; set; }
        public NotePatientModel() { }
    }
}
