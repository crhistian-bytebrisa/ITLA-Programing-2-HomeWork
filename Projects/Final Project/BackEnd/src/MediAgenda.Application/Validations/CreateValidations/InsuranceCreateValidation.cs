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

namespace MediAgenda.Application.Validations.CreateValidations
{
    public class InsuranceCreateValidation : AbstractValidator<InsuranceCreateDTO>
    {
        private readonly IValidationService service;

        public InsuranceCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<InsuranceModel, string>("Name", name)
                ).WithMessage("Ya hay un seguro con ese nombre.");
        }
    }
}
