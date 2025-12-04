using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.UpdateValidations
{
    public class ClinicUpdateValidation : AbstractValidator<ClinicUpdateDTO>
    {
        private readonly IValidationService service;

        public ClinicUpdateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<ClinicModel, int>("Id", id))
                .WithMessage("El Id de la clinica no existe.");

            RuleFor(x => x.Name)
                .MustAsync(async (model, name, ct) =>
                    !await service.ExistsProperty<ClinicModel, string,int>("Name", name,model.Id)
                ).WithMessage("Ya hay una clinica con ese nombre.");
        }

    }
}
