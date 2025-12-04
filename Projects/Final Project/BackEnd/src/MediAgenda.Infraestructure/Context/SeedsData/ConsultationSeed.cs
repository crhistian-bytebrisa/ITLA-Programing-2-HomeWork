using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class ConsultationSeed : IEntityTypeConfiguration<ConsultationModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ConsultationModel> builder)
        {
            builder.HasData(
                new ConsultationModel
                {
                    Id = 1,
                    PatientId = 1,
                    ReasonId = 1,
                    DayAvailableId = 1,
                    State = ConsultationState.Pendent,
                    Turn = 1
                }
            );
        }
    }
}
