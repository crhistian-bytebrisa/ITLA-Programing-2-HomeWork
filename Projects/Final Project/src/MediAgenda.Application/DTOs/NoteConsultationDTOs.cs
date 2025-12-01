using MediAgenda.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MediAgenda.Application.DTOs
{
    public class NoteConsultationDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public ConsultationSimpleDTO Consultation { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NoteConsultationSimpleDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class NoteConsultationCreateDTO 
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int ConsultationId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(2000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Content { get; set; }
    }

    public class NoteConsultationUpdateDTO : NoteConsultationCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime UpdateAt { get; set; }
    }
}
