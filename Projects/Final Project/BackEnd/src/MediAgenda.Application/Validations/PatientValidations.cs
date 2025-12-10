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
    public class PatientCreateValidation : AbstractValidator<PatientCreateDTO>
    {
        private readonly IValidationService service;

        public PatientCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.UserId)
                .MustAsync(async (userid, ct) =>
                    !await service.ExistsProperty<PatientModel, string>("UserId", userid)
                ).WithMessage("Ya existe un usuario con ese Id.");

            RuleFor(x => x.UserId)
                .MustAsync(async (userid, ct) =>
                    await service.ExistsProperty<ApplicationUserModel, string>("Id", userid)
                ).WithMessage("No existe un usuario con ese Id.");

            RuleFor(x => x.InsuranceId)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<InsuranceModel, int>("Id", id)
                ).WithMessage("No existe ese seguro medico.");

            RuleFor(x => x.Identification)
                 .MustAsync(async (identification, ct) =>
                    !await service.ExistsProperty<PatientModel, string>("Identification", identification)
                ).WithMessage("Ya esa cedula esta registrada.");

            RuleFor(x => x.DateOfBirth)
                .MustAsync(async (birth, ct) =>
                {
                    return birth <= DateTime.Now.AddYears(-16);
                })
                .WithMessage("Debe tener al menos 16 años de edad.");
        }
    }

    public class PatientsUpdateValidation : AbstractValidator<PatientUpdateDTO>
    {
        private readonly IValidationService service;

        public PatientsUpdateValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<PatientModel, int>("Id", id)
                ).WithMessage("El Id no existe.");

            RuleFor(x => x.InsuranceId)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<InsuranceModel, int>("Id", id)
                ).WithMessage("No existe un seguro con ese Id.");

            RuleFor(x => x.Identification)
                 .MustAsync(async (identification, ct) =>
                    !await service.ExistsProperty<PatientModel, string>("Identification", identification)
                ).WithMessage("Ya alguien tiene esa cedula.");

            RuleFor(x => x.DateOfBirth)
                .MustAsync(async (birth, ct) =>
                {
                    return birth <= DateTime.Now.AddYears(-16); 
                })
                .WithMessage("Debe tener al menos 16 años de edad.");
        }
    }
}
