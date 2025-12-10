using MediAgenda.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MediAgenda.Application.DTOs
{
    public class NotePatientDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NotePatientSimpleDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NotePatientCreateDTO 
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Content { get; set; }
    }

    public class NotePatientUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Content { get; set; }
    }
}
