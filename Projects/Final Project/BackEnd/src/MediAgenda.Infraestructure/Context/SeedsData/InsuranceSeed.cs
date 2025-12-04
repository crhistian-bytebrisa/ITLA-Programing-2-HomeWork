using MediAgenda.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class InsuranceSeed : IEntityTypeConfiguration<InsuranceModel>
    {
        public void Configure(EntityTypeBuilder<InsuranceModel> builder)
        {
            builder.HasData(
                new InsuranceModel
                {
                    Id = 1,
                    Name = "Humano"
                }
            );
        }
    }
}
