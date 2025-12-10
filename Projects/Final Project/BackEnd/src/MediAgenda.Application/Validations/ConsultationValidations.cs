using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class ConsultationCreateValidation : AbstractValidator<ConsultationCreateDTO>
    {
        private readonly IValidationService service;

        public ConsultationCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.PatientId)
                .MustAsync(async (patientId, ct) =>
                    await service.ExistsProperty<PatientModel, int>("Id", patientId)
                ).WithMessage("El paciente no existe.");

            RuleFor(x => x.DayAvailableId)
                .MustAsync(async (dayId, ct) =>
                    await service.ExistsProperty<DayAvailableModel, int>("Id", dayId)
                ).WithMessage("Ese dia no existe.");

            RuleFor(x => x.DayAvailableId)
                 .MustAsync(async (dayId, ct) =>
                 {
                     var count = await service.GetPropertyById<DayAvailableModel, int, int>("Limit", dayId);
                     var consultations = await service.GetPropertyById<DayAvailableModel, int, List<ConsultationModel>>("Consultations", dayId);

                     if (consultations == null)
                     {
                         return true;
                     }

                     return consultations.Count < count;
                 }                    
                ).WithMessage("Ese dia no esta disponible.");

            RuleFor(x => x.PatientId)
                 .MustAsync(async (model ,patientId, ct) =>
                 {
                     var consultations = await service.GetPropertyById<DayAvailableModel, int, List<ConsultationModel>>("Consultations", model.DayAvailableId);

                     if (consultations == null)
                     {
                         return true;
                     }

                     return !consultations.Any(x => x.PatientId == patientId);
                 }
                ).WithMessage("Ya tiene una consulta ese dia.");

            RuleFor(x => x.ReasonId)
                .MustAsync(async (dayId, ct) =>
                    await service.ExistsProperty<DayAvailableModel, int>("Id", dayId)
                ).WithMessage("La razon no existe.");
        }
    }

    public class ConsultationsUpdateValidation : AbstractValidator<ConsultationUpdateDTO>
    {
        private readonly IValidationService service;

        public ConsultationsUpdateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<ConsultationModel, int>("Id", id)
                ).WithMessage("La consulta no existe.");

            RuleFor(x => x.PatientId)
                .MustAsync(async (patientId, ct) =>
                    await service.ExistsProperty<PatientModel, int>("Id", patientId)
                ).WithMessage("El paciente no existe.");

            RuleFor(x => x.DayAvailableId)
                .MustAsync(async (dayId, ct) =>
                    await service.ExistsProperty<DayAvailableModel, int>("Id", dayId)
                ).WithMessage("Ese dia no existe.");

            RuleFor(x => x.DayAvailableId)
                 .MustAsync(async (dto,dayId, ct) =>
                 {
                     var count = await service.GetPropertyById<DayAvailableModel, int, int>("Limit", dayId);
                     var consultations = await service.GetPropertyById<DayAvailableModel, int, List<ConsultationModel>>("Consultations", dayId);
                     var otherConsultations = consultations.Where(c => c.Id != dto.Id).Count();

                     return otherConsultations < count;
                 }
                ).WithMessage("Ese dia no esta disponible.");

            RuleFor(x => x.ReasonId)
                .MustAsync(async (dayId, ct) =>
                    await service.ExistsProperty<DayAvailableModel, int>("Id", dayId)
                ).WithMessage("La razon no existe.");
        }
    }
}
