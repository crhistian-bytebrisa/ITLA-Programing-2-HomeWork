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
    public class AnalysisSeed : IEntityTypeConfiguration<AnalysisModel>
    {
        public void Configure(EntityTypeBuilder<AnalysisModel> builder)
        {
            builder.HasData(
                new AnalysisModel
                {
                    Id = 1,
                    Name = "Prueba de sangre",
                    Description = "Prueba para ver tus niveles de globulos rojos y blancos ademas de otros datos, no duele."
                },
                new AnalysisModel
                {
                    Id = 2,
                    Name = "Analisis de orina",
                    Description = "Un estandar para ver infecciones.",
                },
                new AnalysisModel
                {
                    Id = 3,
                    Name = "Radiografia",
                    Description = "Imagen para ver fracturas o anomalias a nivel oseo.",
                }
            );
        }
    }
}
