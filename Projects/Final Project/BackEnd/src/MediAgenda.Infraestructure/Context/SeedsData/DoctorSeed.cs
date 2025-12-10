using MediAgenda.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class DoctorSeed : IEntityTypeConfiguration<DoctorModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DoctorModel> builder)
        {
            builder.HasData(
                new DoctorModel
                {
                    Id = 1,
                    UserId = "7bd25c44-f324-41f7-aae9-43a2f744ef46",
                    Specialty = "Cardiologia",
                    AboutMe = "Especialista en cardiologia con 10 años de experiencia."
                }
            );
        }
    }
}
