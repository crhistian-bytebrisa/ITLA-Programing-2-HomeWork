using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class ReasonCreateValidation : AbstractValidator<ReasonCreateDTO>
    {
        private readonly IValidationService service;

        public ReasonCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Title)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<ReasonModel, string>("Title", name)
                ).WithMessage("El titulo ya existe.");
        }
    }

    public class ReasonsUpdateValidation : AbstractValidator<ReasonUpdateDTO>
    {
        private readonly IValidationService service;

        public ReasonsUpdateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<ReasonModel, int>("Id", id)
                ).WithMessage("El Id no existe.");


            RuleFor(x => x.Title)
                .MustAsync(async (model, title, ct) =>
                    !await service.ExistsProperty<ReasonModel, string, int>("Title", title, model.Id)
                ).WithMessage("El titulo ya existe.");
        }
    }

    public class ReasonsPatchValidation : AbstractValidator<ReasonPatchDTO>
    {
        private readonly IValidationService service;
        public ReasonsPatchValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.Title)
                .MustAsync(async (model, title, ct) =>
                    !await service.ExistsProperty<ReasonModel, string, int>("Title", title, model.Id)
                ).WithMessage("El titulo ya existe.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El titulo es obligatorio.")
                .MaximumLength(100).WithMessage("El titulo debe tener como maximo 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripcion debe tener como maximo 500 caracteres.");

            RuleFor(x => x.Available)
                .NotNull().WithMessage("El campo disponible es obligatorio.");

        }
    }
}
