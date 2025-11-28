using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.UpdateValidations
{
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
                .MustAsync(async (model,title, ct) =>
                    !await service.ExistsProperty<ReasonModel, string, int>("Title", title, model.Id)
                ).WithMessage("El titulo ya existe.");
        }
    }
}
