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
    public class NotePatientCreateValidation : AbstractValidator<NotePatientCreateDTO>
    {
        private readonly IValidationService service;

        public NotePatientCreateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.PatientId)
           .MustAsync(async (patientId, ct) =>
               await service.ExistsProperty<PatientModel, int>("Id", patientId)
           ).WithMessage("El paciente no existe.");

            RuleFor(x => x.Title)
                .MustAsync(async (model, tittle, ct) =>
                    !await service.ExistsPropertyAndOtherProperty<NotePatientModel, string, int>("Title", "PatientId", tittle,model.PatientId)
                ).WithMessage("El titulo ya existe.");
        }
    }

    public class NotePatientsUpdateValidation : AbstractValidator<NotePatientUpdateDTO>
    {
        private readonly IValidationService service;

        public NotePatientsUpdateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<NotePatientModel, int>("Id", id)
                ).WithMessage("El Id no existe.");


            RuleFor(x => x.Title)
                .MustAsync(async (model, title, ct) =>
                    {
                        var id = await service.GetPropertyById<NotePatientModel, int,int>("PatientId", model.Id);

                        if(id == null)
                        {
                            return true;
                        }

                        var exists = await service.ExistsPropertyAndOtherPropertyExcludingId<NotePatientModel, string, int,int>("Title", "PatientId", title, id,model.Id);

                        return !exists;
                    }
                   
                ).WithMessage("El titulo ya existe.");
        }
    }
}
