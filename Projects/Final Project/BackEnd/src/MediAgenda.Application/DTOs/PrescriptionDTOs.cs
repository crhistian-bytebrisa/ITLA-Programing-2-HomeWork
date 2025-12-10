using MediAgenda.Application.DTOs.Relations;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public ConsultationSimpleDTO Consultation { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }
        public int AnalysisCount { get; set; }
        public int MedicinesCount { get; set; }
        public int PermissionsCount { get; set; }
    }

    public class PrescriptionSimpleDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }
    }

    public class PrescriptionCreateDTO
    {
        [Required]
        public int ConsultationId { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string GeneralRecomendations { get; set; }
    }
}
