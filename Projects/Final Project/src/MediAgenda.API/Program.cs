
using FluentValidation;
using MediAgenda.API.Middleware;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
using MediAgenda.Application.Validations;
using MediAgenda.Application.Validations.CreateValidations;
using MediAgenda.Application.Validations.UpdateValidations;
using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace MediAgenda.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MediContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddValidatorsFromAssemblyContaining<AnalysisCreateValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<ReasonsUpdateValidation>();
            builder.Services.AddFluentValidationAutoValidation();



            builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
            builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
            builder.Services.AddScoped<IDayAvailableRepository, DayAvailableRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IInsuranceRepository, InsuranceRepository>();
            builder.Services.AddScoped<IMedicalDocumentRepository, MedicalDocumentRepository>();
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            builder.Services.AddScoped<INoteConsultationRepository, NoteConsultationRepository>();
            builder.Services.AddScoped<INotePatientRepository, NotePatientRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            builder.Services.AddScoped<IReasonRepository, ReasonRepository>();

            builder.Services.AddScoped<IAnalysesService, AnalysesService>();
            builder.Services.AddScoped<IReasonsService, ReasonsService>();
            builder.Services.AddScoped<IApplicationUsersService, ApplicationUsersService>();
            builder.Services.AddScoped<IClinicsService, ClinicsService>();
            builder.Services.AddScoped<IDayAvailablesService, DayAvailablesService>();
            builder.Services.AddScoped<IDoctorsService, DoctorsService>();
            builder.Services.AddScoped<IInsurancesService, InsurancesService>();
            builder.Services.AddScoped<IMedicalDocumentsService, MedicalDocumentsService>();

            builder.Services.AddScoped<IValidationService, ValidationService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseLogMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
