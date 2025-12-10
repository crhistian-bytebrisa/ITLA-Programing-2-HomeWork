using FluentValidation;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class RegisterValidations : AbstractValidator<RegisterDTO>
    {
        private readonly IValidationService _service;
        public RegisterValidations(IValidationService service) 
        { 
            _service = service;

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

            RuleFor(x => x.Password)
                .MustAsync(async (password, ct) =>
                {
                    if (string.IsNullOrEmpty(password))
                        return false;

                    bool hasDigit = password.Any(char.IsDigit);
                    bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));
                    bool hasMinLength = password.Length >= 6;

                    return hasDigit && hasSpecial && hasMinLength;

                }).WithMessage("La contraseña debe tener al menos 6 caracteres, numeros y caracteres especiales.");

            RuleFor(x => x.Email)
                .MustAsync(async (email, ct) =>
                    !await service.ExistsProperty<ApplicationUserModel, string>("Email", email)
                ).WithMessage("El correo ya esta en uso.");
        }
    }
}
