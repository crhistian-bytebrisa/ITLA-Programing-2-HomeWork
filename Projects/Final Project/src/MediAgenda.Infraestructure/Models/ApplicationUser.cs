using MediAgenda.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser, IEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(3)]
        public string Nombre { get; set; }

        [Required, MaxLength(150), MinLength(5)]
        public string Apellido { get; set; }

        // Navegation
        public DoctorModel? Doctor { get; set; } = new();
        public PatientModel? Patient { get; set; } = new();

        public ApplicationUser()
        {

        }
    }

    
}
