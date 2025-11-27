using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Validations.RepoValidations;
using MediAgenda.Infraestructure.Models;

public class AnalysisCreateValidation : AbstractValidator<AnalysisCreateDTO>
{
    private readonly RepoIdIntValidation<AnalysisModel> _repoValidation;

    public AnalysisCreateValidation(RepoIdIntValidation<AnalysisModel> repoValidation)
    {
        _repoValidation = repoValidation;

        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) =>
            {
                var exists = await _repoValidation.ExistName(name);
                return !exists;
            }).WithMessage("El nombre ya existe.");
    }
}