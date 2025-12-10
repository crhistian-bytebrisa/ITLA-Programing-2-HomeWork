using Azure.Identity;
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

namespace MediAgenda.Application.Validations
{
    public class InsuranceValidations : AbstractValidator<InsuranceCreateDTO>
    {
        private readonly IValidationService service;

        public InsuranceValidations(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<InsuranceModel, string>("Name", name)
                ).WithMessage("Ya hay un seguro con ese nombre.");
        }
    }

    public class InsuranceUpdateValidation : AbstractValidator<InsuranceUpdateDTO>
    {
        private readonly IValidationService service;

        public InsuranceUpdateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<InsuranceModel, int>("Id", id)
                ).WithMessage("El Id no existe.");

            RuleFor(x => x.Name)
                .MustAsync(async (model, name, ct) =>
                    !await service.ExistsProperty<InsuranceModel, string, int>("Name", name, model.Id)
                ).WithMessage("Ya hay un seguro con ese nombre.");
        }
    }
}
