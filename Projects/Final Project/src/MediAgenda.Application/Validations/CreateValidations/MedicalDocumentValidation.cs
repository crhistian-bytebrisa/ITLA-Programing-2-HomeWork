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

namespace MediAgenda.Application.Validations.CreateValidations
{
    public class MedicalDocumentValidation : AbstractValidator<MedicalDocumentCreateDTO>
    {
        private readonly IValidationService service;

        public MedicalDocumentValidation(IValidationService service)
        {
            this.service = service;

            RuleFor(x => x.PatientId)
                .MustAsync(async (id, ct) =>
                    await service.ExistsProperty<PatientModel, int>("Id", id)
                ).WithMessage("El Id del paciente no existe.");

            RuleFor(x => x.FileName)
                .MustAsync(async (model, name, ct) =>
                    !await service.ExitsPropertyInSameId<MedicalDocumentModel, string, int>("FileName", name,"PatientId",model.PatientId)
                ).WithMessage("El nombre del documento ya existe.");

            RuleFor(x => x.File)
                .MustAsync(async (file, ct) =>
                {
                    var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".jpg", ".png" };
                    var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    return allowedExtensions.Contains(extension);
                }).WithMessage("El formato del archivo no es válido. Los formatos permitidos son: .pdf, .doc, .docx, .jpg, .png");

            RuleFor(x => x.File)
               .NotNull()
               .WithMessage("Debe seleccionar un archivo.");

            RuleFor(x => x.File)
                .MustAsync(async (file, ct) =>
                {
                    const long maxFileSize = 10 * 1024 * 1024; // 10 MB
                    return file.Length <= maxFileSize;
                }).WithMessage("El tamaño del archivo excede el límite permitido de 10 MB.");
        }
    }
}
