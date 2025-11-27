using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Validations.RepoValidations;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.CreateValidations
{
    public class ApplicationUserCreateValidation : AbstractValidator<ApplicationUserCreateDTO>
    {
        private readonly RepoIdStringValidation<ApplicationUserModel> _repoValidation;

        public ApplicationUserCreateValidation(RepoIdStringValidation<ApplicationUserModel> repoValidation)
        {
            _repoValidation = repoValidation;

            RuleFor(x => x.UserName)
                .MustAsync(async (name, ct) =>
                {
                    var exists = await _repoValidation.ExitsUserName(name);
                    return !exists;
                }).WithMessage("El usuario no es valido.");

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
                {
                    var exists = await _repoValidation.ExitsEmail(email);
                    return !exists;
                }).WithMessage("El correo ya esta en uso.");
        }
    }
}
