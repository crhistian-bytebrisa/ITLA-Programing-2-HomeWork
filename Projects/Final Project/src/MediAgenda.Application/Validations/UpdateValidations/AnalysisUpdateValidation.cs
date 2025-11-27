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
    public class AnalysisUpdateValidation : AbstractValidator<AnalysisUpdateDTO>
    {
        private readonly RepoIdIntValidation<AnalysisModel> _repoValidation;

        public AnalysisUpdateValidation(RepoIdIntValidation<AnalysisModel> repoValidation)
        {
            _repoValidation = repoValidation;

            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                {
                    var exists = await _repoValidation.ExistsAsync(id);
                    return exists;
                }).WithMessage("El Id no existe.");

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                {
                    var exists = await _repoValidation.ExistName(name);
                    return !exists;
                }).WithMessage("El nombre ya existe.");
        }
    }
}
