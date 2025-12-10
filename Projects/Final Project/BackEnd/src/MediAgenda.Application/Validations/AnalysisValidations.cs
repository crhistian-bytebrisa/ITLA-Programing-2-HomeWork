using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;

namespace MediAgenda.Application.Validations
{
    public class AnalysisCreateValidation : AbstractValidator<AnalysisCreateDTO>
    {
        private readonly IValidationService service;

        public AnalysisCreateValidation(IValidationService service)
        {

            this.service = service;

            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<AnalysisModel, string>("Name", name))
                .WithMessage("El nombre ya existe.");
        }
    }
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