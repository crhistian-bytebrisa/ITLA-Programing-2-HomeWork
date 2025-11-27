using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Validations.RepoValidations;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.UpdateValidations
{
    public class ReasonsUpdateValidation : AbstractValidator<ReasonUpdateDTO>
    {
        private readonly RepoIdIntValidation<ReasonModel> _repoValidation;

        public ReasonsUpdateValidation(RepoIdIntValidation<ReasonModel> repoValidation)
        {
            _repoValidation = repoValidation;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                {
                    var exists = await _repoValidation.ExistsAsync(id);
                    return exists;
                }).WithMessage("El Id no existe.");

            RuleFor(x => x.Title)
                .MustAsync(async (name, ct) =>
                {
                    var exists = await _repoValidation.ExitsTitle(name);
                    return !exists;
                }).WithMessage("El titulo ya existe.");
        }
    }
}
