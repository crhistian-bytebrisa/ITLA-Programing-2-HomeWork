using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class PrescriptionCreateValidation : AbstractValidator<PrescriptionCreateDTO>
    {
        private readonly IValidationService service;

        public PrescriptionCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.ConsultationId)
                .MustAsync(async (cId, ct) =>
                    await service.ExistsProperty<ConsultationModel, int>("Id", cId)
                ).WithMessage("No existe esa consulta.");

            RuleFor(x => x.ConsultationId)
                .MustAsync(async (cId, ct) =>
                    !await service.ExistsProperty<PrescriptionModel, int>("ConsultationId", cId)
                ).WithMessage("Ya existe una prescripcion con esa consulta.");
        }
    }
}
