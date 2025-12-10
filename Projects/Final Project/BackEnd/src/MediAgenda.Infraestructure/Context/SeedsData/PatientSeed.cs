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
    public class PatientSeed : IEntityTypeConfiguration<PatientModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PatientModel> builder)
        {
            builder.HasData(
                new PatientModel
                {
                    Id = 1,
                    UserId = "1ebe636e-b277-47ea-a2f8-6502cd988476",
                    InsuranceId = 1,
                    Identification = "40214353345",
                    DateOfBirth = new DateTime(2004, 1, 25),
                    Bloodtype = Bloodtype.O_Positive,
                    Gender = Gender.Male                    
                },
                new PatientModel
                {
                    Id = 2,
                    UserId = "12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46",
                    InsuranceId = 1,
                    Identification = "40243453345",
                    DateOfBirth = new DateTime(2004, 5, 30),
                    Bloodtype = Bloodtype.O_Negative,
                    Gender = Gender.Female
                },
                new PatientModel
                {
                    Id = 3,
                    UserId = "12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf",
                    InsuranceId = 1,
                    Identification = "40234533343",
                    DateOfBirth = new DateTime(2000, 3, 6),
                    Bloodtype = Bloodtype.A_Positive,
                    Gender = Gender.Male
                });
        }
    }

}
