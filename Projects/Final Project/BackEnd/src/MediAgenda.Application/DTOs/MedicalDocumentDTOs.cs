using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class MedicalDocumentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientSimpleDTO Patient { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
    }

    public class MedicalDocumentSimpleDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
    }

    public class MedicalDocumentCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public string FileName { get; set; }
    }

    public class MedicalDocumentCreateWithUrlDTO : MedicalDocumentCreateDTO
    {
        public required string FileUrl { get; set; }
        public required string DocumentType { get; set; }
    }
}
