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
    public class MedicalDocumentModel : IEntityInt, IFileName
    {
        [Key]
        public int Id { get; set; }

        [Required,ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientModel Patient { get; set; }

        [Required, MinLength(8)]
        public string FileName { get; set; }

        [Required,MinLength(10)]
        public string FileUrl { get; set; }

        [Required, MinLength(3), MaxLength(25)]
        public string DocumentType { get; set; }
        public MedicalDocumentModel()
        {
                
        }
    }
}
