using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Validations;
using MediAgenda.Infraestructure.Models;

public class AnalysisCreateValidation : AbstractValidator<AnalysisCreateDTO>
{
    private readonly RepoValidation<AnalysisModel> _repoValidation;

    public AnalysisCreateValidation(RepoValidation<AnalysisModel> repoValidation)
    {
        _repoValidation = repoValidation;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El {PropertyName} no puede tener mas de 100 caracteres.")
            .MinimumLength(8).WithMessage("El {PropertyName} debe tener al menos 8 caracteres.")
            .MustAsync(async (name, ct) =>
            {
                var exists = await _repoValidation.ExistName(name);
                return !exists;
            }).WithMessage("El nombre ya existe.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La {PropertyName} no puede tener mas de 500 caracteres.");
    }
}