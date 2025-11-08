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

            builder.Entity<PrescriptionMedicineModel>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });

            builder.Entity<PrescriptionAnalysisModel>()
                .HasKey(pa => new { pa.PrescriptionId, pa.AnalysisId });

            builder.Entity<PrescriptionPermissionModel>()
                .HasKey(pp => new { pp.PrescriptionId, pp.PermissionId });

            builder.Entity<CurrentMedicamentsModel>()
                .HasKey(cm => new { cm.PatientId, cm.MedicineId });

            base.OnModelCreating(builder);
        }

        //Users
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //Clinic
        public DbSet<InsuranceModel> Insurances { get; set; }
        public DbSet<ClinicModel> Clinics { get; set; }
        public DbSet<DayAvailableModel> DaysAvailable { get; set; }
        public DbSet<CurrentMedicamentsModel> CurrentMedicaments { get; set; }
        public DbSet<MedicalDocumentModel> MedicalDocuments { get; set; }

        //Prescriptions

        public DbSet<MedicineModel> Medicines { get; set; }
        public DbSet<PrescriptionModel> Prescriptions { get; set; }
        public DbSet<AnalysisModel> Analyses { get; set; }
        public DbSet<PermissionModel> Permissions { get; set; }
        public DbSet<PrescriptionAnalysisModel> PrescriptionAnalyses { get; set; }
        public DbSet<PrescriptionMedicineModel> PrescriptionMedicines { get; set; }
        public DbSet<PrescriptionPermissionModel> PrescriptionPermissions { get; set; }

        //Consultations and notes

        public DbSet<ConsultationModel> Consultations { get; set; }
        public DbSet<ReasonModel> Reasons { get; set; }
        public DbSet<NotePatientModel> NotePatients { get; set; }
        public DbSet<NoteConsultationModel> NoteConsultations { get; set; }
        }
}
