using FluentValidation;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
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
        private readonly IValidationService service;

        public DayAvailableCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.ClinicId)
                .MustAsync(async (id, ct) =>
                    !await service.ExistsProperty<DayAvailableModel,int>("ClinicId",id))
                .WithMessage("El Id de la clinica no existe.");

            RuleFor(x => x)
                .MustAsync(async (entity, validationContext, ct) =>
                    !await service.ExistsEqualDateAndTime<DayAvailableModel>(entity.Date, entity.StartTime, entity.EndTime))
                .WithMessage(x => $"El horario seleccionado tiene choque con algunos horarios.");
        }
    }
}
