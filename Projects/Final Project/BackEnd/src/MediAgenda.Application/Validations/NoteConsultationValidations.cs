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
    public class NoteConsultationCreateValidation : AbstractValidator<NoteConsultationCreateDTO>
    {
        private readonly IValidationService service;

        public NoteConsultationCreateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.ConsultationId)
           .MustAsync(async (patientId, ct) =>
               await service.ExistsProperty<ConsultationModel, int>("Id", patientId)
           ).WithMessage("El paciente no existe.");

            RuleFor(x => x.Title)
                .MustAsync(async (model, tittle, ct) =>
                    !await service.ExistsPropertyAndOtherProperty<NoteConsultationModel, string, int>("Title", "ConsultationId", tittle, model.ConsultationId)
                ).WithMessage("El titulo ya existe.");
        }
    }

    public class NoteConsultationsUpdateValidation : AbstractValidator<NoteConsultationUpdateDTO>
    {
        private readonly IValidationService service;

        public NoteConsultationsUpdateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<NoteConsultationModel, int>("Id", id)
                ).WithMessage("El Id no existe.");


            RuleFor(x => x.Title)
                .MustAsync(async (model, title, ct) =>
                {
                    var id = await service.GetPropertyById<NoteConsultationModel, int, int>("ConsultationId", model.Id);

                    if (id == null)
                    {
                        return true;
                    }

                    var exists = await service.ExistsPropertyAndOtherPropertyExcludingId<NoteConsultationModel, string, int, int>("Title", "ConsultationId", title, id, model.Id);

                    return !exists;
                }

                ).WithMessage("El titulo ya existe.");
        }
    }
}