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
    public class DoctorValidations : AbstractValidator<DoctorCreateDTO>
    {
        private readonly IValidationService service;

        public DoctorValidations(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.UserId)
                .MustAsync(async (userId, ct) =>
                    await service.ExistsProperty<ApplicationUserModel, string>("Id", userId)
                ).WithMessage("El usuario no existe.");
        }
    }

    public class DoctorUpdateValidation : AbstractValidator<DoctorUpdateDTO>
    {
        private readonly IValidationService service;

        public DoctorUpdateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<DoctorModel, int>("Id", id))
                .WithMessage("El Id del doctor no existe.");

            RuleFor(x => x.UserId)
                .MustAsync(async (userId, ct) =>
                    await service.ExistsProperty<ApplicationUserModel, string>("Id", userId)
                ).WithMessage("El usuario no existe.");
        }
    }
}
