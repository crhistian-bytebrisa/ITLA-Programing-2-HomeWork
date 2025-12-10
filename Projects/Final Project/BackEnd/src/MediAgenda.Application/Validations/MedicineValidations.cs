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
    public class MedicineCreateValidation : AbstractValidator<MedicineCreateDTO>
    {
        private readonly IValidationService service;

        public MedicineCreateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Name)
                .MustAsync(async (name, ct) =>
                    !await service.ExistsProperty<MedicineModel, string>("Name", name)
                ).WithMessage("El nombre ya existe.");
        }
    }

    public class MedicinesUpdateValidation : AbstractValidator<MedicineUpdateDTO>
    {
        private readonly IValidationService service;

        public MedicinesUpdateValidation(IValidationService service)
        {
            this.service = service;
            RuleFor(x => x.Id)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<MedicineModel, int>("Id", id)
                ).WithMessage("El Id no existe.");
        }
    }
}
