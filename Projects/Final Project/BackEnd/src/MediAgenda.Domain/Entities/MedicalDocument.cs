using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class MedicalDocument : Entity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string DocumentType { get; set; }
        public MedicalDocument()
        {
                
        }
    }
}
