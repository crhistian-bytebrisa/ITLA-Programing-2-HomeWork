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

namespace MediAgenda.Application.Validations.CreateValidations
{
    public class ClinicCreateValidation : AbstractValidator<ClinicCreateDTO>
    {
        private readonly IValidationService service;

        public ClinicCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<ClinicModel, string>("Name", name)
                ).WithMessage("Ya hay una clinica con ese nombre.");
        }

    }
}
