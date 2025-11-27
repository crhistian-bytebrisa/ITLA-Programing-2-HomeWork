using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class AnalysisDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrescriptionAnalysesCount { get; set; }
    }

    public class AnalysisSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AnalysisCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}")]
        public string Description { get; set; }
    }

    public class AnalysisUpdateDTO 
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Description { get; set; }
    }
}
