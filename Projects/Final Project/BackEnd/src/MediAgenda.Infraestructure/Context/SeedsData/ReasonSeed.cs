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
    public class ReasonSeed : IEntityTypeConfiguration<ReasonModel>
    {
        public void Configure(EntityTypeBuilder<ReasonModel> builder)
        {
            builder.HasData(
                new ReasonModel
                {
                    Id = 1,
                    Title = "Consulta"
                },
                new ReasonModel
                {
                    Id = 2,
                    Title = "Primera vez"
                },
                new ReasonModel
                {
                    Id = 3,
                    Title = "Permiso"
                },
                new ReasonModel
                {
                    Id = 4,
                    Title = "Referimiento"
                },
                new ReasonModel
                {
                    Id = 5,
                    Title = "Entrega de Estudios"
                });
        }
    }
}
