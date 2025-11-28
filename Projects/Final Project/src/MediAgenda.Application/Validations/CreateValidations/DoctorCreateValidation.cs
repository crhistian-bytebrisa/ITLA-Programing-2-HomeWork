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
    public class DoctorCreateValidation : AbstractValidator<DoctorCreateDTO>
    {
        private readonly IValidationService service;

        public DoctorCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.UserId)
                .MustAsync(async (userId, ct) =>
                    await service.ExistsProperty<ApplicationUserModel, string>("Id", userId)
                ).WithMessage("El usuario no existe.");
        }
    }
}
