using MediAgenda.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class DayAvailableSeed : IEntityTypeConfiguration<DayAvailableModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DayAvailableModel> builder)
        {
            builder.HasData(
                new DayAvailableModel
                {
                    Id = 1,
                    ClinicId = 1,
                    Date = DateOnly.FromDateTime(new DateTime(2025,10,21)),
                    StartTime = new TimeOnly(8,0),
                    EndTime = new TimeOnly(16, 0),
                    Limit = 15
                }
            );
        }
    }

}
