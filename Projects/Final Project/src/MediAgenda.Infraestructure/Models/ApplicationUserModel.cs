using MediAgenda.Domain.Core;
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
    public class ApplicationUserModel : IdentityUser, IHasUsername
    {

        [Required, MaxLength(200), MinLength(7)]
        public string NameComplete { get; set; }

        // Navegation
        public DoctorModel? Doctor { get; set; } 
        public PatientModel? Patient { get; set; } 

        public ApplicationUserModel()
        {

        }
    }

    
}
