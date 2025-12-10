using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class ClinicSeed : IEntityTypeConfiguration<ClinicModel>
    {
        public void Configure(EntityTypeBuilder<ClinicModel> builder)
        {
            builder.HasData(
                new ClinicModel
                {
                    Id = 1,
                    Name = "Clinica 9",
                    Address = "KM 9 de la Autopista Duarte",
                    PhoneNumber = "8095475432"
                });
        }
                
    }
}
