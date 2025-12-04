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

namespace MediAgenda.Application.Validations.UpdateValidations
{
    public class AnalysisUpdateValidation : AbstractValidator<AnalysisUpdateDTO>
    {
        private readonly IValidationService servie;

        public AnalysisUpdateValidation(IValidationService servie)
        {

            this.servie = servie;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await servie.ExistsProperty<AnalysisModel, int>("Id", id)
                ).WithMessage("El Id no existe.");

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await servie.ExistsProperty<AnalysisModel, string>("Name", name)
                ).WithMessage("El nombre ya existe.");
        }
    }
}
