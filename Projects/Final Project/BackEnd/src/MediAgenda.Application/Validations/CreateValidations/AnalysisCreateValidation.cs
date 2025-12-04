using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;

public class AnalysisCreateValidation : AbstractValidator<AnalysisCreateDTO>
{
    private readonly IValidationService service;

    public AnalysisCreateValidation( IValidationService service )
    {

        this.service = service;

        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) =>
                !await service.ExistsProperty<AnalysisModel, string>("Name", name))
            .WithMessage("El nombre ya existe.");
    }
}