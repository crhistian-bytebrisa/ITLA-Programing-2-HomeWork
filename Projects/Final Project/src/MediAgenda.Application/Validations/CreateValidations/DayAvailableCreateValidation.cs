using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Validations.RepoValidations;
using MediAgenda.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.CreateValidations
{
    public class DayAvailableCreateValidation : AbstractValidator<DayAvailableCreateDTO>
    {
        private readonly RepoIdIntValidation<DayAvailableModel> _repoValidation;
        private readonly RepoIdIntValidation<ClinicModel> _repoClinic;

        public DayAvailableCreateValidation(RepoIdIntValidation<DayAvailableModel> repoValidation, RepoIdIntValidation<ClinicModel> repoClinic)
        {
            _repoValidation = repoValidation;
            _repoClinic = repoClinic;

            RuleFor(x => x.ClinicId)
                .MustAsync(async (id, ct) =>
                {
                    bool exists = await _repoClinic.ExistsAsync(id);
                    return exists;
                }).WithMessage("El Id de la clinica no existe.");

            RuleFor(x => x)
                .MustAsync(async (entity, validationContext, ct) =>
                {
                    var t = await _repoValidation.TimeValidation(entity.Date, entity.StartTime, entity.EndTime);

                    return !t;
                })
                .WithMessage(x => $"El horario seleccionado tiene choque con algunos horarios.");
        }
    }
}
