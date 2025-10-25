using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Context
{
    public class MediContext : IdentityDbContext
    {
        public MediContext() { }
        public MediContext(DbContextOptions<MediContext> options) : base(options)   
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Persist Security Info=False;Trusted_Connection=True;database=MediAgenda;server=(local);TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<PrescriptionMedicine>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });

            builder.Entity<PrescriptionAnalysis>()
                .HasKey(pa => new { pa.PrescriptionId, pa.AnalysisId });

            builder.Entity<PrescriptionPermission>()
                .HasKey(pp => new { pp.PrescriptionId, pp.PermissionId });

            base.OnModelCreating(builder);
        }

        //Users
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //Clinic
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DayAvailable> DaysAvailable { get; set; }  

        //Prescriptions

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PrescriptionAnalysis> PrescriptionAnalyses { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<PrescriptionPermission> PrescriptionPermissions { get; set; }

        //Consultations and notes

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<NotePatient> NotePatients { get; set; }
        public DbSet<NoteConsultation> NoteConsultations { get; set; }
        }
}
