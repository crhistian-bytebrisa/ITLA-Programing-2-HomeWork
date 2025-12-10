using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class NoteConsultation : Entity
    {
        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public NoteConsultation() { }
    }
}
