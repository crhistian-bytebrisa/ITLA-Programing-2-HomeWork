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
    public class ReasonCreateValidation : AbstractValidator<ReasonCreateDTO>
    {
        private readonly RepoValidation<ReasonModel> _repoValidation;

        public ReasonCreateValidation(RepoValidation<ReasonModel> repoValidation)
        {
            _repoValidation = repoValidation;

            RuleFor(x => x.Title)
                .MustAsync(async (name, ct) =>
                {
                    var exists = await _repoValidation.ExitsTitle(name);
                    return !exists;
                }).WithMessage("El titulo ya existe.");
        }
    }
}
