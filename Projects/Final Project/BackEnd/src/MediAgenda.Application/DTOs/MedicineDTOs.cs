using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Core;
using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public bool IsActive { get; set; }
        public int PrescriptionMedicinesCount { get; set; }
        public int CurrentMedicamentsCount { get; set; }
    }

    public class MedicineSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
    }

    public class MedicineCreateDTO 
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public string Format { get; set; }
    }

    public class MedicineUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool IsActive { get; set; } = true;
    }
}
