using MediAgenda.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Doctor : Entity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Specialty { get; set; }
        public string AboutMe { get; set; }

        public Doctor()
        {

        }
    }

}
