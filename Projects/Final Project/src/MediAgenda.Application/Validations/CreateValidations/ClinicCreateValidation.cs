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
    public class ClinicCreateValidation : AbstractValidator<ClinicCreateDTO>
    {
        private readonly RepoValidation<ClinicModel> _repoValidation;

        public ClinicCreateValidation(RepoValidation<ClinicModel> repoValidation)
        {
            _repoValidation = repoValidation;

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                {
                    var exists = await _repoValidation.ExistName(name);
                    return !exists;
                }).WithMessage("El usuario no es valido.");
        }

    }
}
