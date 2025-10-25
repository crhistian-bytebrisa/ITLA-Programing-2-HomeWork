using MediAgenda.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Map
        public Doctor? Doctor { get; set; } = new();
        public Patient? Patient { get; set; } = new();

        public ApplicationUser()
        {

        }
    }

    
}
