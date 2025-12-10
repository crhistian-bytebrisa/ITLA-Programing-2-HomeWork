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
    public class MedicineSeed : IEntityTypeConfiguration<MedicineModel>
    {
        public void Configure(EntityTypeBuilder<MedicineModel> builder)
        {
            builder.HasData(
                new MedicineModel
                {
                    Id = 1,
                    Name = "Paracetamol",
                    Description = "An analgesic and antipyretic medication used to treat pain and fever.",
                    Format = "Tablet",
                    IsActive = true
                },
                new MedicineModel
                {
                    Id = 2,
                    Name = "Ibuprofen",
                    Description = "A nonsteroidal anti-inflammatory drug (NSAID) used to reduce inflammation, pain, and fever.",
                    Format = "Capsule",
                    IsActive = true
                },
                new MedicineModel
                {
                    Id = 3,
                    Name = "Amoxicillin",
                    Description = "A penicillin-type antibiotic used to treat a variety of bacterial infections.",
                    Format = "Tablet",
                    IsActive = true
                }
            );
        }
    }
}
