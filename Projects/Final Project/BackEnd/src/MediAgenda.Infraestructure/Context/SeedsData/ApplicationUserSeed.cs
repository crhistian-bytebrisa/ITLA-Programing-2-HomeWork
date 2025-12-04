using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context.SeedsData
{
    public class ApplicationUserSeed : IEntityTypeConfiguration<ApplicationUserModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserModel> builder)
        {
            builder.HasData(
                new ApplicationUserModel
                {
                    Id = "7bd25c44-f324-41f7-aae9-43a2f744ef46",
                    UserName = "dr.martinez@mediagenda.com",
                    NormalizedUserName = "DR.MARTINEZ@MEDIAGENDA.COM",
                    Email = "dr.martinez@mediagenda.com",
                    NormalizedEmail = "DR.MARTINEZ@MEDIAGENDA.COM",
                    NameComplete = "Carlos Martínez Pérez",
                    PhoneNumber = "8091234567",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEO6wwBvPFDvL8ZtmHuLxxm92JC0+LnLYNVEnNmUhLyBn3fDPJV1AhvVGwJcG5FxJg==",
                    SecurityStamp = "DOCTOR-SECURITY-STAMP-001",
                    ConcurrencyStamp = "doctor-concurrency-001"
                },
                new ApplicationUserModel
                {
                    Id = "1ebe636e-b277-47ea-a2f8-6502cd988476",
                    UserName = "pedro@mediagenda.com",
                    NormalizedUserName = "PEDRO@MEDIAGENDA.COM",
                    Email = "pedro@mediagenda.com",
                    NormalizedEmail = "PEDRO@MEDIAGENDA.COM",
                    NameComplete = "Pedro Alcachofa",
                    PhoneNumber = "8093454567",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEKpB0X8LqNkHXdXJ7c4VnM8WfQ3jqY6P5rZ9K2mT1vU4xW7sY8pL3gH9nR5tA6bC2w==",
                    SecurityStamp = "PEDRO-SECURITY-STAMP-002",
                    ConcurrencyStamp = "pedro-concurrency-002"
                },
                new ApplicationUserModel
                {
                    Id = "12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46",
                    UserName = "alva@mediagenda.com",
                    NormalizedUserName = "ALVA@MEDIAGENDA.COM",
                    Email = "alva@mediagenda.com",
                    NormalizedEmail = "ALVA@MEDIAGENDA.COM",
                    NameComplete = "Alva Alcachofa",
                    PhoneNumber = "8093543337",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAELmN4Y9QrS6tH2XkP8fV1nW7jY3mZ5pK9rL4xT8vU2wQ6sA7cB1dE3gF5hI9jK0lMn==",
                    SecurityStamp = "ALVA-SECURITY-STAMP-003",
                    ConcurrencyStamp = "alva-concurrency-003"
                },
                new ApplicationUserModel
                {
                    Id = "12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf",
                    UserName = "rufino@mediagenda.com",
                    NormalizedUserName = "RUFINO@MEDIAGENDA.COM",
                    Email = "rufino@mediagenda.com",
                    NormalizedEmail = "RUFINO@MEDIAGENDA.COM",
                    NameComplete = "Rufino Alcachofa",
                    PhoneNumber = "8491782495",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEPzC8Y3RsT7uI9YmQ1gW2oX8kZ4nA6qM0sL5yU9xV3wR7tB2eC4fD6hG8iJ1kL3mNo==",
                    SecurityStamp = "RUFINO-SECURITY-STAMP-004",
                    ConcurrencyStamp = "rufino-concurrency-004"
                }
            );
        }
    }

    public class RoleSeed : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "admin-role-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "admin-role-stamp-001" 
                },
                new IdentityRole
                {
                    Id = "doctor-role-id",
                    Name = "Doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = "doctor-role-stamp-002"
                },
                new IdentityRole
                {
                    Id = "user-role-id",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "user-role-stamp-003" 
                });
        }
    }
    public class UserRoleSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "7bd25c44-f324-41f7-aae9-43a2f744ef46", // Dr. Carlos Martínez Pérez
                    RoleId = "doctor-role-id"
                },
                new IdentityUserRole<string>
                {
                    UserId = "1ebe636e-b277-47ea-a2f8-6502cd988476", // Pedro Alcachofa
                    RoleId = "user-role-id"
                },
                new IdentityUserRole<string>
                {
                    UserId = "12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46", // Alva Alcachofa
                    RoleId = "user-role-id"
                },
                new IdentityUserRole<string>
                {
                    UserId = "12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf", // Rufino Alcachofa
                    RoleId = "user-role-id"
                });
        }
    }
}
    